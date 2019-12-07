using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using teh13th.HandleUtility.Enums;
using teh13th.HandleUtility.Structs;
using teh13th.HandleUtility.Tools;
using ExcludeFromCodeCoverage = System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute;

namespace teh13th.HandleUtility
{
	/// <summary>
	/// Utility for working with Windows handles.
	/// </summary>
	[PublicAPI]
	public static class HandleUtility
	{
		private static readonly ProcessNameGetter ProcessNameGetter = new ProcessNameGetter();

		/// <summary>
		/// Gets all opened handles for <paramref name="filePath"/> with owner process info.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <returns>Info about handles to file.</returns>
		[NotNull, ContractAnnotation("null => halt")]
		public static IEnumerable<FileHandle> GetHandlesForFile([NotNull] string filePath)
		{
			if (filePath is null)
			{
				throw new ArgumentNullException(nameof(filePath));
			}

			if (string.IsNullOrWhiteSpace(filePath))
			{
				throw new ArgumentException("Value cannot be empty or whitespace.", nameof(filePath));
			}

			using var handlesCountSafeHandle = GetHandleCountPointer();
			var handlesCountPointer = handlesCountSafeHandle.DangerousGetHandle();

			var handlesCount = IntPtr.Size == 4
								   ? Marshal.ReadInt32(handlesCountPointer)
								   : Marshal.ReadInt64(handlesCountPointer);

			var offset = IntPtr.Size * 2;
			var sizeOfSystemHandleEntry = Marshal.SizeOf(typeof(SystemHandleTableEntryInfoEx));

			for (var i = (long)handlesCountPointer + offset + ((handlesCount - 1) * sizeOfSystemHandleEntry);
				 i > (long)handlesCountPointer + offset;
				 i -= sizeOfSystemHandleEntry)
			{
				var systemHandleEntry =
					(SystemHandleTableEntryInfoEx)Marshal.PtrToStructure(new IntPtr(i), typeof(SystemHandleTableEntryInfoEx));

				if (systemHandleEntry.ObjectTypeIndex is 37 /* File */)
				{
					var handleFilePath = GetFilePathForFileHandle(systemHandleEntry.Handle,
																  (int)systemHandleEntry.OwnerProcessId,
																  (AccessMask)systemHandleEntry.GrantedAccess);

					if (handleFilePath != null)
					{
						if (handleFilePath.Equals(filePath, StringComparison.OrdinalIgnoreCase))
						{
							yield return new FileHandle(handleFilePath,
														systemHandleEntry.Handle,
														systemHandleEntry.GrantedAccess,
														(int)systemHandleEntry.OwnerProcessId,
														ProcessNameGetter);
						}
					}
				}
			}
		}

		[NotNull, ExcludeFromCodeCoverage]
		private static SafeFileHandle GetHandleCountPointer()
		{
			var length = 10000000;

			while (true)
			{
				var handleCountPointer = Marshal.AllocHGlobal(length);

				var result = NativeMethods.NtQuerySystemInformation(SystemInformationClass.SystemExtendedHandleInformation,
																	handleCountPointer,
																	length,
																	out var wantedLength);

				switch (result)
				{
					case NtStatus.InfoLengthMismatch:
						length = Math.Max(length, wantedLength);
						Marshal.FreeHGlobal(handleCountPointer);
						break;
					case NtStatus.Success:
						return new SafeFileHandle(handleCountPointer, true);
					default:
						Marshal.FreeHGlobal(handleCountPointer);
						throw new Exception("Failed to retrieve system handle information.");
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), CanBeNull]
		private static string GetFilePathForFileHandle(IntPtr handle, int processId, AccessMask grantedAccess)
		{
			using var processHandle = NativeMethods.OpenProcess(ProcessAccessRights.ProcessDupHandle, false, processId);
			if (processHandle.IsInvalid)
			{
				return null;
			}

			using var handleDuplicate = DuplicateFileHandle(processHandle, handle);
			return handleDuplicate.IsInvalid ? null : GetFilePathByHandle(handleDuplicate, processId, grantedAccess);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining), NotNull]
		private static SafeFileHandle DuplicateFileHandle([NotNull] SafeProcessHandle processHandle, IntPtr handle)
		{
			NativeMethods.DuplicateHandle(processHandle,
										  handle,
										  NativeMethods.GetCurrentProcess(),
										  out var handleDuplicate,
										  0 /* no access */,
										  false,
										  0 /* no options */);

			return handleDuplicate;
		}

		[CanBeNull]
		private static string GetFilePathByHandle([NotNull] SafeFileHandle handle,
												  int processId,
												  AccessMask grantedAccess)
		{
			string filepath = null;

			var thread = new Thread(() => { filepath = GetFilePathFromHandle(handle); });
			thread.Start();

			var result = thread.Join(100);
			if (!result)
			{
				var processName = ProcessNameGetter.GetProcessNameById(processId);

				Console.WriteLine(
					$"Failed to get file path for handle 0x{handle.DangerousGetHandle().ToInt32():X} of process {processName} [{processId}] Granted access = {grantedAccess:F} ({grantedAccess:X}).");
			}

			return filepath;
		}

		[CanBeNull, ExcludeFromCodeCoverage]
		private static string GetFilePathFromHandle([NotNull] SafeFileHandle handle)
		{
			FileNameInfo fileInfo = default;

			var bufferSize = Marshal.SizeOf(fileInfo) + (NativeMethods.FileNameCapacity * 2);
			var pointerToBuffer = Marshal.AllocHGlobal(bufferSize);

			try
			{
				Marshal.StructureToPtr(fileInfo, pointerToBuffer, false);

				NativeMethods.GetFileInformationByHandleEx(
					handle,
					FileInfoByHandleClass.FileNameInfo,
					out fileInfo,
					bufferSize);

				try
				{
					return fileInfo.FileName.Length != 0 ? Path.GetFullPath(fileInfo.FileName) : null;
				}
				catch (NotSupportedException)
				{
					return null;
				}
			}
			finally
			{
				Marshal.FreeHGlobal(pointerToBuffer);
			}
		}
	}
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using Microsoft.Win32.SafeHandles;
using teh13th.HandleUtility.Enums;
using teh13th.HandleUtility.Structs;

namespace teh13th.HandleUtility.Tools;

[ExcludeFromCodeCoverage]
internal static class NativeMethods
{
	private const string NtdllDll = "ntdll.dll";
	private const string Kernel32Dll = "kernel32.dll";

	public const int FileNameCapacity = 1000;

	[DllImport(NtdllDll, CharSet = CharSet.Auto, SetLastError = true)]
	[ResourceExposure(ResourceScope.None)]
	public static extern NtStatus NtQuerySystemInformation(
		[In] SystemInformationClass systemInformationClass,
		[In] IntPtr systemInformation,
		[In] int systemInformationLength,
		[Out] out int returnedLength);

	[DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
	[ResourceExposure(ResourceScope.Process), SuppressUnmanagedCodeSecurity, SecurityCritical]
	public static extern SafeProcessHandle OpenProcess(
		[In] ProcessAccessRights desiredAccess,
		[In] bool inheritHandle,
		[In] int processId);

	[DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
#if !NET5_0_OR_GREATER
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
#endif
	[ResourceExposure(ResourceScope.Process), SuppressUnmanagedCodeSecurity, SecurityCritical, SecuritySafeCritical]
	public static extern bool DuplicateHandle(
		[In] SafeProcessHandle sourceProcessHandle,
		[In] IntPtr sourceHandle,
		[In] SafeProcessHandle targetProcessHandle,
		[Out] out SafeFileHandle targetHandle,
		[In] int desiredAccess,
		[In] bool inheritHandle,
		[In] int options);

	[DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
#if !NET5_0_OR_GREATER
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
#endif
	[ResourceExposure(ResourceScope.Process), SuppressUnmanagedCodeSecurity, SecurityCritical, SecuritySafeCritical]
	public static extern SafeProcessHandle GetCurrentProcess();

	[DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool GetFileInformationByHandleEx(
		[In] SafeFileHandle file,
		[In] FileInfoByHandleClass fileInformationClass,
		[Out] out FileNameInfo fileInformation,
		[In] int bufferSize);
}
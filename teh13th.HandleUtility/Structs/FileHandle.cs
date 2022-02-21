using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using teh13th.HandleUtility.Enums;
using teh13th.HandleUtility.Interfaces;

namespace teh13th.HandleUtility.Structs;

/// <summary>
/// Information about file handle.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly struct FileHandle
{
	/// <summary>
	/// File path.
	/// </summary>
	public readonly string FilePath;

	/// <summary>
	/// Handle.
	/// </summary>
	public readonly SafeFileHandle Handle;

	/// <summary>
	/// Access mask.
	/// </summary>
	public readonly AccessMask GrantedAccess;

	/// <summary>
	/// Owner process ID.
	/// </summary>
	public readonly int ProcessId;

	/// <summary>
	/// Owner process name.
	/// </summary>
	public readonly string ProcessName;

	internal FileHandle(
		string filePath,
		IntPtr handle,
		uint grantedAccess,
		int processId,
		IProcessNameGetter processNameGetter)
	{
		Handle = new SafeFileHandle(handle, false);
		GrantedAccess = (AccessMask)grantedAccess;
		FilePath = filePath;
		ProcessId = processId;
		ProcessName = processNameGetter.GetProcessNameById(processId);
	}

	/// <inheritdoc />
	public override string ToString()
	{
		return $"Handle: 0x{Handle.DangerousGetHandle().ToInt32():X}. " +
			$"Owner: {ProcessName} [{ProcessId}]. " +
			$"Access: {GrantedAccess:F}.";
	}
}
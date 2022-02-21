using System;
using System.Runtime.InteropServices;

namespace teh13th.HandleUtility.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct SystemHandleTableEntryInfoEx
{
	public IntPtr Object;
	public IntPtr OwnerProcessId;
	public IntPtr Handle;
	public uint GrantedAccess;
	public ushort CreatorBackTraceIndex;
	public ushort ObjectTypeIndex;
	public uint Attributes;
	public uint Reserved;
}
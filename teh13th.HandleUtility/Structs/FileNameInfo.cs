using System.Runtime.InteropServices;
using teh13th.HandleUtility.Tools;

namespace teh13th.HandleUtility.Structs;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct FileNameInfo
{
	public uint FileNameLength;

	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = NativeMethods.FileNameCapacity)]
	public string FileName;
}
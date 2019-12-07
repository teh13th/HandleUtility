using System;
using JetBrains.Annotations;

namespace teh13th.HandleUtility.Enums
{
	[Flags, PublicAPI]
	internal enum ProcessAccessRights : uint
	{
		// standard rights

		/// <summary>
		/// Required to delete the object
		/// </summary>
		Delete = 0x00010000,

		/// <summary>
		/// Required to read information in the security descriptor for the object, not including the information in the SACL. To read or write the SACL, you must request the ACCESS_SYSTEM_SECURITY access right
		/// </summary>
		ReadControl = 0x00020000,

		/// <summary>
		/// Required to modify the DACL in the security descriptor for the object
		/// </summary>
		WriteDac = 0x00040000,

		/// <summary>
		/// Required to change the owner in the security descriptor for the object
		/// </summary>
		WriteOwner = 0x00080000,

		/// <summary>
		/// Required to wait for the process to terminate using the wait functions
		/// </summary>
		Synchronize = 0x00100000,

		// special process rights

		/// <summary>
		/// Required to create a process
		/// </summary>
		ProcessCreateProcess = 0x00000080,

		/// <summary>
		/// Required to create a thread
		/// </summary>
		ProcessCreateThread = 0x00000002,

		/// <summary>
		/// Required to duplicate a handle using DuplicateHandle
		/// </summary>
		ProcessDupHandle = 0x00000040,

		/// <summary>
		/// Required to retrieve certain information about a process, such as its token, exit code, and priority class
		/// </summary>
		ProcessQueryInformation = 0x00000400,

		/// <summary>
		/// Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName). A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION
		/// </summary>
		ProcessQueryLimitedInformation = 0x00001000,

		/// <summary>
		/// Required to set certain information about a process, such as its priority class
		/// </summary>
		ProcessSetInformation = 0x00000200,

		/// <summary>
		/// Required to set memory limits using SetProcessWorkingSetSize
		/// </summary>
		ProcessSetQuota = 0x00000100,

		/// <summary>
		/// Required to suspend or resume a process
		/// </summary>
		ProcessSuspendResume = 0x00000800,

		/// <summary>
		/// Required to terminate a process using TerminateProcess
		/// </summary>
		ProcessTerminate = 0x00000001,

		/// <summary>
		/// Required to perform an operation on the address space of a process
		/// </summary>
		ProcessVmOperation = 0x00000008,

		/// <summary>
		/// Required to read memory in a process using ReadProcessMemory
		/// </summary>
		ProcessVmRead = 0x00000010,

		/// <summary>
		/// Required to write to memory in a process using WriteProcessMemory
		/// </summary>
		ProcessVmWrite = 0x00000020,

		/// <summary>
		/// All possible access rights for a process object
		/// </summary>
		ProcessAllAccess = Delete | ReadControl | WriteDac | WriteOwner | Synchronize | 0xFFF
	}
}
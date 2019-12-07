using System;
using JetBrains.Annotations;

#pragma warning disable 1591

namespace teh13th.HandleUtility.Enums
{
	[Flags, PublicAPI]
	public enum AccessMask : uint
	{
		// Specific rights

		/// <summary>
		/// For a file object, the right to read the corresponding file data. For a directory object, the right to read the corresponding directory data
		/// </summary>
		FileReadData = 0x00000001,

		/// <summary>
		/// For a file object, the right to write data to the file. For a directory object, the right to create a file in the directory (FILE_ADD_FILE)
		/// </summary>
		FileWriteData = 0x00000002,

		/// <summary>
		/// For a file object, the right to append data to the file. (For local files, write operations will not overwrite existing data if this flag is specified without FILE_WRITE_DATA.) For a directory object, the right to create a subdirectory (FILE_ADD_SUBDIRECTORY)
		/// </summary>
		FileAppendData = 0x00000004,

		/// <summary>
		/// The right to read extended file attributes
		/// </summary>
		FileReadEa = 0x00000008,

		/// <summary>
		/// The right to write extended file attributes.
		/// </summary>
		FileWriteEa = 0x00000010,

		/// <summary>
		/// For a native code file, the right to execute the file. This access right given to scripts may cause the script to be executable, depending on the script interpreter
		/// </summary>
		FileExecute = 0x00000020,

		/// <summary>
		/// For a directory, the right to delete a directory and all the files it contains, including read-only files
		/// </summary>
		FileDeleteChild = 0x00000040,

		/// <summary>
		/// The right to read file attributes
		/// </summary>
		FileReadAttributes = 0x00000080,

		/// <summary>
		/// The right to write file attributes
		/// </summary>
		FileWriteAttributes = 0x00000100,

		/*
		Unknown01 = 0x00000200,
		Unknown02 = 0x00000400,
		Unknown03 = 0x00000800,
		Unknown04 = 0x00001000,
		Unknown05 = 0x00002000,
		Unknown06 = 0x00004000,
		Unknown07 = 0x00008000,
		*/

		// Standard rights

		/// <summary>
		/// The right to delete the object
		/// </summary>
		Delete = 0x00010000,

		/// <summary>
		/// The right to read the information in the object's security descriptor, not including the information in the system access control list (SACL)
		/// </summary>
		ReadControl = 0x00020000,

		/// <summary>
		/// The right to modify the discretionary access control list (DACL) in the object's security descriptor
		/// </summary>
		WriteDac = 0x00040000,

		/// <summary>
		/// The right to change the owner in the object's security descriptor
		/// </summary>
		WriteOwner = 0x00080000,

		/// <summary>
		/// The right to use the object for synchronization. This enables a thread to wait until the object is in the signaled state. Some object types do not support this access right
		/// </summary>
		Synchronize = 0x00100000,

		/*
		Unknown08 = 0x00200000,
		Unknown09 = 0x00400000,
		Unknown10 = 0x00800000,
		*/

		/// <summary>
		/// The right to get or set the SACL in the object security descriptor
		/// </summary>
		AccessSystemSecurity = 0x01000000,

		MaximumAllowed = 0x02000000,

		// Reserved
		/*
		Unknown11 = 0x04000000,
		Unknown12 = 0x08000000,
		*/

		GenericAll = 0x10000000,
		GenericExecute = 0x20000000,
		GenericWrite = 0x40000000,
		GenericRead = 0x80000000,

		SpecificRightsAll = 0x0000FFFF,

		/// <summary>
		/// Currently defined to equal READ_CONTROL
		/// </summary>
		StandardRightsRead = ReadControl,

		/// <summary>
		/// Currently defined to equal READ_CONTROL
		/// </summary>
		StandardRightsWrite = ReadControl,

		/// <summary>
		/// Currently defined to equal READ_CONTROL
		/// </summary>
		StandardRightsExecute = ReadControl,

		/// <summary>
		/// Combines DELETE, READ_CONTROL, WRITE_DAC, and WRITE_OWNER access
		/// </summary>
		StandardRightsRequired = Delete | ReadControl | WriteDac | WriteOwner,

		/// <summary>
		/// Combines DELETE, READ_CONTROL, WRITE_DAC, WRITE_OWNER, and SYNCHRONIZE access
		/// </summary>
		StandardRightsAll = Delete | ReadControl | WriteDac | WriteOwner | Synchronize,

		Owner = FileReadData
				| FileWriteData
				| FileAppendData
				| FileReadEa
				| FileWriteEa
				| FileExecute
				| FileDeleteChild
				| FileReadAttributes
				| FileWriteAttributes
				| Delete
				| ReadControl
				| WriteDac
				| WriteOwner
				| Synchronize,

		ReadOnly = FileReadData
					| FileReadEa
					| FileExecute
					| FileReadAttributes
					| ReadControl
					| Synchronize,

		Contributor = Owner & ~(FileDeleteChild | WriteDac | WriteOwner)
	}
}
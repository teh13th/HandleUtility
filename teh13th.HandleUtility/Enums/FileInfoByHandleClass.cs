using JetBrains.Annotations;

namespace teh13th.HandleUtility.Enums
{
	[PublicAPI]
	internal enum FileInfoByHandleClass : uint
	{
		FileBasicInfo,
		FileStandardInfo,
		FileNameInfo,
		FileRenameInfo,
		FileDispositionInfo,
		FileAllocationInfo,
		FileEndOfFileInfo,
		FileStreamInfo,
		FileCompressionInfo,
		FileAttributeTagInfo,
		FileIdBothDirectoryInfo,
		FileIdBothDirectoryRestartInfo,
		FileIoPriorityHintInfo,
		FileRemoteProtocolInfo,
		FileFullDirectoryInfo,
		FileFullDirectoryRestartInfo,
		FileStorageInfo,
		FileAlignmentInfo,
		FileIdInfo,
		FileIdExtdDirectoryInfo,
		FileIdExtdDirectoryRestartInfo,
		FileDispositionInfoEx,
		FileRenameInfoEx,
		MaximumFileInfoByHandleClass,
		FileCaseSensitiveInfo,
		FileNormalizedNameInfo
	}
}
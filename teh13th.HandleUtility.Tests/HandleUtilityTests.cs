using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace teh13th.HandleUtility.Tests
{
	[TestClass]
	public sealed class HandleUtilityTests
	{
		private const int TestTimeout = 10000;

		[TestMethod, Timeout(TestTimeout), TestCategory("Disabled")]
		public void GetHandlesForFile_Correct_WhenValidFilePathGiven()
		{
			var testFilePath = Path.Combine(Path.GetTempPath(), "1232465465d1.tmp");

			try
			{
				using (File.Open(testFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
				{
					var handles = HandleUtility.GetHandlesForFile(testFilePath).ToArray();

					handles.Should().NotBeNull();
					handles.Should().HaveCount(1);
				}
			}
			finally
			{
				File.Delete(testFilePath);
			}
		}

		[TestMethod, Timeout(TestTimeout)]
		[SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
		[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
		public void GetHandlesForFile_ThrowsException_WhenNullFilePathGiven()
		{
			Action act = () => HandleUtility.GetHandlesForFile(null).ToArray();
			act.Should().ThrowExactly<ArgumentNullException>();
		}

		[DataTestMethod, Timeout(TestTimeout)]
		[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
		public void GetHandlesForFile_ThrowsException_WhenEmptyFilePathGiven()
		{
			Action act = () => HandleUtility.GetHandlesForFile(string.Empty).ToArray();
			act.Should().ThrowExactly<ArgumentException>();
		}

		[DataTestMethod, Timeout(TestTimeout)]
		[SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
		public void GetHandlesForFile_ThrowsException_WhenWhitespaceFilePathGiven()
		{
			Action act = () => HandleUtility.GetHandlesForFile("    ").ToArray();
			act.Should().ThrowExactly<ArgumentException>();
		}
	}
}
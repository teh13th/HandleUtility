using System;
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

		[TestMethod, Timeout(TestTimeout), Ignore]
		public void GetHandlesForFile_Correct_WhenValidFilePathGiven()
		{
			var testFilePath = Path.GetTempFileName();

			try
			{
				using (File.Open(testFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
				{
					var handles = HandleUtility.GetHandlesForFile(testFilePath).ToArray();

					handles.Should().NotBeNull();
					handles.Should().HaveCountGreaterThanOrEqualTo(1);
				}
			}
			finally
			{
				File.Delete(testFilePath);
			}
		}

		[TestMethod, Timeout(TestTimeout)]
		public void GetHandlesForFile_ThrowsException_WhenNullFilePathGiven()
		{
			Action act = () => _ = HandleUtility.GetHandlesForFile(null!).ToArray();
			act.Should().ThrowExactly<ArgumentNullException>();
		}

		[DataTestMethod, Timeout(TestTimeout)]
		public void GetHandlesForFile_ThrowsException_WhenEmptyFilePathGiven()
		{
			Action act = () => _ = HandleUtility.GetHandlesForFile(string.Empty).ToArray();
			act.Should().ThrowExactly<ArgumentException>();
		}

		[DataTestMethod, Timeout(TestTimeout)]
		public void GetHandlesForFile_ThrowsException_WhenWhitespaceFilePathGiven()
		{
			Action act = () => _ = HandleUtility.GetHandlesForFile("    ").ToArray();
			act.Should().ThrowExactly<ArgumentException>();
		}
	}
}
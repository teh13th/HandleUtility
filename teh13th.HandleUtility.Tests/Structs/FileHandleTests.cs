using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using teh13th.HandleUtility.Interfaces;
using teh13th.HandleUtility.Structs;

namespace teh13th.HandleUtility.Tests.Structs
{
	[TestClass]
	public sealed class FileHandleTests
	{
		private const int TestTimeout = 1000;

		[TestMethod, Timeout(TestTimeout)]
		[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
		public void Constructor_Success_WhenCalled()
		{
			var mock = new Mock<IProcessNameGetter>();
			mock.Setup(x => x.GetProcessNameById(It.IsAny<int>())).Returns("process");

			Action act = () => new FileHandle("path", new IntPtr(1), 1, 1, mock.Object);

			act.Should().NotThrow();
			mock.Verify(x => x.GetProcessNameById(It.IsAny<int>()), Times.Once);
		}

		[TestMethod, Timeout(TestTimeout)]
		public void ToString_Correct_WhenCalled()
		{
			var mock = new Mock<IProcessNameGetter>();
			mock.Setup(x => x.GetProcessNameById(It.IsAny<int>())).Returns("process");

			var handle = new FileHandle("path", new IntPtr(2), 1, 3, mock.Object);

			handle.ToString().Should().Be("Handle: 0x2. Owner: process [3]. Access: FileReadData.");
		}
	}
}
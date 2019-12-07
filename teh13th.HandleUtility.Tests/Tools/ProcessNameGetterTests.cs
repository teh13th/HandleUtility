using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using teh13th.HandleUtility.Tools;

namespace teh13th.HandleUtility.Tests.Tools
{
	[TestClass]
	public sealed class ProcessNameGetterTests
	{
		private const int TestTimeout = 2000;

		[TestMethod, Timeout(TestTimeout)]
		public void GetProcessNameById_Success_WhenValidIdGiven()
		{
			var currentProcess = Process.GetCurrentProcess();
			new ProcessNameGetter().GetProcessNameById(currentProcess.Id).Should().Be(currentProcess.ProcessName);
		}

		[TestMethod, Timeout(TestTimeout)]
		public void GetProcessNameById_ReturnsUnknown_WhenInvalidIdGiven()
		{
			new ProcessNameGetter().GetProcessNameById(-1).Should().Be("UNKNOWN");
		}
	}
}
using System.Diagnostics;
using System.Runtime.CompilerServices;
using teh13th.HandleUtility.Interfaces;

namespace teh13th.HandleUtility.Tools
{
	internal sealed class ProcessNameGetter : IProcessNameGetter
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetProcessNameById(int id)
		{
			try
			{
				using var process = Process.GetProcessById(id);
				return process.ProcessName;
			}
			catch
			{
				return "UNKNOWN";
			}
		}
	}
}
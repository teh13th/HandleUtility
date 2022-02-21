using System.Diagnostics;
using teh13th.HandleUtility.Interfaces;

namespace teh13th.HandleUtility.Tools;

internal sealed class ProcessNameGetter : IProcessNameGetter
{
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
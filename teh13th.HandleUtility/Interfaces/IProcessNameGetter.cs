using JetBrains.Annotations;

namespace teh13th.HandleUtility.Interfaces
{
	internal interface IProcessNameGetter
	{
		[NotNull]
		string GetProcessNameById(int id);
	}
}
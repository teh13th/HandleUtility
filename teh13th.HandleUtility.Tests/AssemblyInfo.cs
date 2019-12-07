using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: ComVisible(false)]

[assembly: Guid("d2ad3d7d-503b-465c-b48c-55abc3dbab87")]

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
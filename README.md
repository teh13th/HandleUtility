# HandleUtility

[![Build status](https://dev.azure.com/teh13th/HandleUtility/_apis/build/status/teh13th.HandleUtility)](https://dev.azure.com/teh13th/HandleUtility/_build/latest?definitionId=4)
![Release status](https://vsrm.dev.azure.com/teh13th/_apis/public/Release/badge/79d2174a-c89a-48b3-921b-dd17b458298c/1/1)
![Code coverage](https://img.shields.io/azure-devops/coverage/teh13th/HandleUtility/4)
![Nuget version](https://img.shields.io/nuget/v/teh13th.HandleUtility)
![Nuget downloads](https://img.shields.io/nuget/dt/teh13th.HandleUtility)

Utility for working with Windows handles.

## Usage

For a list of all opened handles to file, use the following code:

```csharp
using teh13th.HandleUtility;
IEnumerable<FileHandle> handles = HandleUtility.GetHandlesForFile(path_to_file);
```

`FileHandle` structure contains the following info about handle:

- Path to the file the handle refers to
- Handle
- Access granted to the handle
- ID of the handle owner process
- Name of the handle owner process

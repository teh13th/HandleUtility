# HandleUtility

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

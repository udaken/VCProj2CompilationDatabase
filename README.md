# VCProj2CompilationDatabase

Visual C++ 2008 Project File(.vcproj) To clang Compilation database(compile_commands.json) Convertor.

## Requirements

### To Use

- Visual Studio 2008 Installed. (using VCProjectEngineLibrary.dll)

### To Build

- Visual Studio 2017 or later.

## Usage

```
VCProj2CompilationDatabase.exe

  --buildtarget               (Default: Release|Win32) Configuration and Platform. e.g. Release|Win32

  -h, --skipheader            Skip header file.

  -u, --utf8                  Convert file encoding to UTF8withBOM.

  -p, --printplatforms        Print installed platforms.

  --help                      Display this help screen.

  --version                   Display version information.

  projectfilepath (pos. 0)    Required.
```

## License

MIT License

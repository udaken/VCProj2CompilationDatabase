# VCProj2CompilationDatabase

Visual C++ 2008 Project File(.vcproj) To clang Compilation database(compile_commands.json) Convertor.

## Requirements

### To Use

- Visual Studio 2008 Installed. (using VCProjectEngineLibrary.dll)

### To Build

- Visual Studio 2017 or later.

## Usage

```
VCProj2CompilationDatabase.exe <path to vcproj file>

  --buildtarget               (Default: Release|Win32) Configuration and Platform. e.g. Release|Win32

  -h, --skipheaders            Skip header files.

  -u, --utf8                  Only Convert file encoding to UTF8withBOM.

  --printplatforms        Print installed platforms.

  --help                      Display this help screen.

  --version                   Display version information.

```

## License

MIT License

## TODO

- Upgrade to .Net Core.
- Separate to system includes and user includes.
- Support PropertySheets.

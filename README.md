# VCProj2CompilationDatabase

Visual C++ 2008 Project File(.vcproj) To clang Compilation database(compile_commands.json) Convertor.

## Requirements

### To Use

- Visual Studio 2008 Installed. (using VCProjectEngine.dll)
- Build.

### To Build

- Visual Studio 2017 or later.

## Usage

```
VCProj2CompilationDatabase.exe <path to vcproj file> [--buildtarget <Configuration|Platform>]

  --buildtarget               (Default: Release|Win32) Configuration and Platform. e.g. Release|Win32

  -i, --includesheader        Include header files.

  -u, --utf8                  Only Convert file encoding to UTF8withBOM.

  -a, --autoutf8              Convert file encoding to UTF8withBOM on the fly.

  -w, --nowwarnaserror        

  --printplatforms            Print installed platforms.

  --printclangcommand         

  --help                      Display this help screen.

  --version                   Display version information.

```

## License

MIT License

## TODO

- Upgrade to .Net Core.
- Separate to system includes and user includes.
- Support PropertySheets.

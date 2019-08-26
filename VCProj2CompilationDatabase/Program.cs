using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using VCProjectEngineLibrary;
using WCE = VCProjectWCEPlatformLibrary;
using static System.Console;
using System.Text.Json;
using System.IO;
using CommandLine;
using System.Text.RegularExpressions;
using System.Collections;

namespace VCProj2json
{
    static class Program
    {
        // https://github.com/microsoft/VCSamples/blob/master/VC2008Samples/Extensibility/ProjectModel/VCProjEngine_CS/Class1.cs
        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.vcprojectengine.vcprojectengineobject?view=visualstudiosdk-2017

        static int Main(string[] args)
        {
            int result = -1;
            CommandLine.Parser.Default.ParseArguments<ProgramOptions>(args)
             .WithParsed(opt =>
             {
                 result = MainAsync(opt).Result;
             });
            return result;
        }

        static void PrintPlatforms(VCProjectEngineObject engine)
        {
            foreach (VCPlatform p in engine.Platforms as IVCCollection)
            {
                Console.WriteLine($"<{p.Name}>");
                Console.WriteLine($"IncludeDirectories:{p.IncludeDirectories}");
                Console.WriteLine($"ExecutableDirectories:{p.ExecutableDirectories}");
                Console.WriteLine($"LibraryDirectories:{p.LibraryDirectories}");
                Console.WriteLine($"SourceDirectories:{p.SourceDirectories}");
                Console.WriteLine($"ExcludeDirectories:{p.ExcludeDirectories}");
                for (var i = 0; i < p.NumberOfPlatformMacros; i++)
                {
                    WriteLine($"PlatformMacro:{p.PlatformMacro[i]}");
                }

                foreach (var tool in p.Tools as IVCCollection)
                {
                    switch (tool)
                    {
                        case VCLinkerTool linkerTool:
                            break;
                        case VCLibrarianTool librarianTool:
                            break;
                        case VCCLCompilerTool compilerTool:
                            Console.WriteLine(compilerTool.toolName);
                            break;
                        case VCCustomBuildTool customBuildTool:
                            break;
                        case VCResourceCompilerTool resourceCompilerTool:
                            break;
                    }
                }
            }
        }

        static async Task<int> MainAsync(ProgramOptions options)
        {
            var engine = new VCProjectEngineObject();
            if (options.PrintPlatforms)
            {
                PrintPlatforms(engine);
                return 0;
            }

            var dir = Path.GetDirectoryName(options.ProjectFilePath);
            var baseJsonPath = Path.Combine(dir, "compile_commands.base.json");
            var baseObj = File.Exists(baseJsonPath) ?
                JsonSerializer.Deserialize<FileCompilationInfo>(File.ReadAllBytes(baseJsonPath)) : new FileCompilationInfo();
            if (baseObj.arguments != null && baseObj.command != null)
            {
                Console.Error.WriteLine("You must specify either `arguments` or `command` in compile_commands.base.json. ");
                return 2;
            }
            baseObj.directory = dir;

            var output = new List<FileCompilationInfo>();
            var project = engine.LoadProject(options.ProjectFilePath) as VCProject;
            if (project == null)
            {
                Console.Error.WriteLine("Invalid project file.");
                return 3;
            }

            //var buildTarget = "Release|Win32";
            //var buildTarget = "Release|Windows Mobile 5.0 Pocket PC SDK (ARMV4I)";
            var buildTarget = options.BuildTarget;

            foreach (VCFile file in project.Files)
            {
                var fileConfigurations = file.FileConfigurations as IVCCollection;
                VCFileConfiguration fileConfig = fileConfigurations.Item(buildTarget);
                if (fileConfig == null)
                {
                    Console.Error.WriteLine($"warning: Build Target [{buildTarget}] Not Found for `{file.Name}`");
                    continue;
                }

                var platform = buildTarget.Split('|')[1];
                var m = new Regex(@"\((\w+)\)").Match(platform);
                var arch =
                    platform == "Win64" ? "x86_64" :
                    m.Success ? m.Groups[1].Value.ToLower() :
                    "i386";

                var entry = ProcessVCFile(options, baseObj.Clone(), file, fileConfig, platform == "Win64");
                if (entry != null)
                {
                    output.Add(entry);
                }
            }

            using (var stream = File.OpenWrite(Path.Combine(dir, CompileCommandsJson)))
            {
                stream.SetLength(0);
                await JsonSerializer.SerializeAsync(stream, output, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    IgnoreNullValues = true,
                });
            }

            return 0;
        }

        const string CompileCommandsJson = "compile_commands.json";
        const string VCCLCompilerToolName = "VCCLCompilerTool";

        static void AddPath(FileCompilationInfo entry, string prefix, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            foreach (var s in path?.Split(Path.PathSeparator))
            {
                entry.AddArg(prefix + QuatePath(s));
            }
        }

        static Encoding UTF8BomEncoding = new UTF8Encoding(true);
        static void ConverToUtf8(string path)
        {
            using (var source = File.OpenRead(path))
            {
                var bom = UTF8BomEncoding.GetPreamble();
                var head = new byte[bom.Length];
                var readCount = source.Read(head, 0, head.Length);
                if (readCount == head.Length && Enumerable.SequenceEqual(bom, head))
                {
                    return;
                }
            }

            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, File.ReadAllText(path, Encoding.Default), UTF8BomEncoding);

            File.Replace(tempFilePath, path, path + ".orig", true);
        }

        static string StripCurrentDir(string path) => path.StartsWith(".\\", StringComparison.Ordinal) ? path.Substring(2) : path;
        static string QuatePath(string path) => "\"" + path + "\"";

        static FileCompilationInfo ProcessVCFile(ProgramOptions options, FileCompilationInfo entry, VCFile file, VCFileConfiguration fileConfig, bool isWin64)
        {
            //WriteLine($"{file.RelativePath}: {file.FileType},{file.Kind},{file.SubType},{file.ItemName},{file.UnexpandedRelativePath}");
            if (fileConfig.ExcludedFromBuild)
            {
                Console.Error.WriteLine($"warning: `{file.Name}` Excluded FromBuild.");
                return null;
            }

            switch (file.FileType)
            {
                case eFileType.eFileTypeCppClass:
                case eFileType.eFileTypeCppCode:
                case eFileType.eFileTypeCppControl:
                case eFileType.eFileTypeCppForm:
                case eFileType.eFileTypeCppHeader:
                case eFileType.eFileTypeCppWebService:
                    break;
                default:
                    return null;
            }
            var isHeader = (file.FileType == eFileType.eFileTypeCppHeader);

            if (isHeader && options.SkipHeader)
                return null;

            WriteLine(file.RelativePath);
            if (options.ConvertToUtf8)
            {
                ConverToUtf8(file.FullPath);
            }

            entry.file = StripCurrentDir(file.RelativePath);

            VCConfiguration projectConfig = fileConfig.ProjectConfiguration;
            var projectTool = projectConfig.Tools.Item(VCCLCompilerToolName) is WCE.VCCLWCECompilerToolBase ?
                new WCECompilerToolWrapper(projectConfig.Tools.Item(VCCLCompilerToolName)) :
                projectConfig.Tools.Item(VCCLCompilerToolName) as VCCLCompilerTool;

            var fileTool = fileConfig.Tool is WCE.VCCLWCECompilerToolBase ?
                new WCECompilerToolWrapper(fileConfig.Tool) :
                fileConfig.Tool as VCCLCompilerTool;

            // if file is header, filetool is null.
            var tool = fileTool ?? projectTool;

            entry.AddArg(isWin64 ? "-m64" : "-m32");

            AddPath(entry, "-D ", fileConfig.Evaluate(projectTool.PreprocessorDefinitions + fileTool?.PreprocessorDefinitions) ?? "");
            AddPath(entry, "-U ", fileConfig.Evaluate(projectTool.UndefinePreprocessorDefinitions + fileTool?.UndefinePreprocessorDefinitions) ?? "");
            AddPath(entry, "-I ", tool.FullIncludePath);
            AddPath(entry, "-include ", fileConfig.Evaluate(tool.ForcedIncludeFiles ?? projectTool.ForcedIncludeFiles));

            if (isHeader)
            {
                entry.AddArg("-S"); // Only run preprocess and compilation steps
                // fsyntax-only
            }
            else
            {
                entry.AddArg("-c"); // Only run preprocess, compile, and assemble steps

                entry.output = Path.Combine(fileConfig.Evaluate(tool.ObjectFile), Path.ChangeExtension(file.Name, ".obj"));
                entry.AddArg("-o");
                entry.AddArg(QuatePath(entry.output));
            }

            switch (tool.WarningLevel)
            {
                case warningLevelOption.warningLevel_4:
                    entry.AddArg("-Wall");
                    entry.AddArg("-Wextra");
                    entry.AddArg("-Wno-undef");
                    entry.AddArg("-Wno-c++11-extensions");
                    entry.AddArg("-Wno-unknown-pragmas");

                    //entry.AddArg("-Wno-old-style-cast");
                    //entry.AddArg("-Wno-gnu-anonymous-struct");
                    //entry.AddArg("-Wno-gnu-zero-variadic-macro-arguments");
                    //entry.AddArg("-Wno-reserved-id-macro");
                    //entry.AddArg("-Wno-c++11-long-long");
                    //entry.AddArg("-Wno-language-extension-token");
                    //entry.AddArg("-Wno-non-virtual-dtor");
                    //entry.AddArg("-Wno-dollar-in-identifier-extension");
                    //entry.AddArg("-Wno-zero-length-array");
                    break;
                case warningLevelOption.warningLevel_3:
                case warningLevelOption.warningLevel_2:
                case warningLevelOption.warningLevel_1:
                    entry.AddArg("-Wall");

                    break;
            }

            if (tool.WarnAsError)
                entry.AddArg("-Werror");

            switch (tool.Optimization)
            {
                case optimizeOption.optimizeFull:
                    entry.AddArg("-O3");
                    break;
                case optimizeOption.optimizeMaxSpeed:
                    entry.AddArg("-O2");
                    break;
                case optimizeOption.optimizeMinSpace:
                    entry.AddArg("-O1");
                    break;
            }

            if (tool.DebugInformationFormat != debugOption.debugDisabled)
                entry.AddArg("-g");

            entry.AddArg(tool.RuntimeTypeInfo ? "-frtti" : "-fno-rtti");

            switch (tool.ExceptionHandling)
            {
                case cppExceptionHandling.cppExceptionHandlingYesWithSEH:
                    entry.AddArg("-fseh-exceptions");
                    if(tool.CompileAs == CompileAsOptions.compileAsCPlusPlus)
                        entry.AddArg("-fcxx-exceptions");
                    break;
                case cppExceptionHandling.cppExceptionHandlingYes:
                    entry.AddArg("-fcxx-exceptions");
                    break;
                case cppExceptionHandling.cppExceptionHandlingNo:
                    entry.AddArg("-fno-cxx-exceptions");
                    break;
            }

            switch (tool.InlineFunctionExpansion)
            {
                case inlineExpansionOption.expandDisable:
                    entry.AddArg("-fno-inline-functions");
                    break;
                case inlineExpansionOption.expandOnlyInline:
                    entry.AddArg("-finline-hint-functions");
                    break;
                case inlineExpansionOption.expandAnySuitable:
                    entry.AddArg("-finline-functions");
                    break;
            }

            if (tool.EnableIntrinsicFunctions)
                entry.AddArg("-fbuiltin");

            switch (tool.CompileAs)
            {
                case CompileAsOptions.compileAsC:
                    entry.AddArg("-x c");
                    entry.AddArg("-std=c99");
                    break;
                case CompileAsOptions.compileAsCPlusPlus:
                    entry.AddArg("-x c++");
                    entry.AddArg("-std=c++03");
                    break;
            }

            {
                string msvcVer = default;
                (file.project as VCProject).Version(out var major, out var minor);
                switch ($"{ major}.{minor}")
                {
                    case "8.0":
                        msvcVer = "14.00";
                        break;
                    case "7.1":
                        msvcVer = "13.10";
                        break;
                    case "7.0":
                        msvcVer = "13.00";
                        break;
                    default:
                    case "9.0":
                        msvcVer = "15.00";
                        break;
                }
                entry.AddArg("-fms-compatibility-version=" + msvcVer);
            }

            switch (tool.CallingConvention)
            {
                case callingConventionOption.callConventionCDecl:
                    //entry.AddArg("-fdefault-calling-conv=cdecl");
                    break;
                case callingConventionOption.callConventionStdCall:
                    entry.AddArg("-mrtd");
                    //entry.AddArg("-fdefault-calling-conv=stdcall");
                    break;
                case callingConventionOption.callConventionFastCall:
                    Console.Error.WriteLine($"warning: Not supported CallingConvention: {tool.CallingConvention}.");
                    //entry.AddArg("-fdefault-calling-conv=fastcall");
                    break;
            }

            string crt = default;
            switch (tool.RuntimeLibrary)
            {
                case runtimeLibraryOption.rtMultiThreaded:
                    entry.AddArg("-D_MT");
                    crt = "--dependent-lib=libcmt";
                    break;

                case runtimeLibraryOption.rtMultiThreadedDebug:
                    entry.AddArg("-D_MT");
                    crt = "--dependent-lib=libcmtd";
                    break;

                case runtimeLibraryOption.rtMultiThreadedDLL:
                    entry.AddArg("-D_MT");
                    entry.AddArg("-D_DLL");
                    crt = "--dependent-lib=msvcrt";
                    break;

                case runtimeLibraryOption.rtMultiThreadedDebugDLL:
                    entry.AddArg("-D_MT");
                    entry.AddArg("-D_DLL");
                    crt = "--dependent-lib=msvcrtd";
                    break;
            }
            if (tool.OmitDefaultLibName)
            {
                entry.AddArg("-D_VC_NODEFAULTLIB");
            }
            else
            {
                //TODO entry.AddArg(crt);
            }

            if (!tool.DisableLanguageExtensions)
            {
                entry.AddArg("-fms-extensions");
                entry.AddArg("-Wno-microsoft-goto");
                entry.AddArg("-Wno-microsoft-exception-spec");
                entry.AddArg("-Wno-microsoft-enum-value");
                entry.AddArg("-Wno-microsoft-comment-paste");
                entry.AddArg("-Wno-microsoft-cast");
                entry.AddArg("-Wno-microsoft-anon-tag");
            }

            if (tool.BufferSecurityCheck)
                entry.AddArg("-fstack-protector");

            if (tool.ShowIncludes)
                entry.AddArg("--trace-includes");

            if (tool.UndefineAllPreprocessorDefinitions)
                entry.AddArg("-undef");

            switch (tool.StructMemberAlignment)
            {
                case structMemberAlignOption.alignSingleByte:
                    entry.AddArg("-fpack-struct=1");
                    break;
                case structMemberAlignOption.alignTwoBytes:
                    entry.AddArg("-fpack-struct=2");
                    break;
                case structMemberAlignOption.alignFourBytes:
                    entry.AddArg("-fpack-struct=4");
                    break;
                case structMemberAlignOption.alignEightBytes:
                    entry.AddArg("-fpack-struct=8");
                    break;
                case structMemberAlignOption.alignSixteenBytes:
                    entry.AddArg("-fpack-struct=16");
                    break;
                case structMemberAlignOption.alignNotSet:
                    break;
            }

            entry.AddArg(QuatePath(entry.file));

            switch (tool.floatingPointModel)
            {
                case floatingPointModel.FloatingPointFast:
                    entry.AddArg("-ffast-math");
                    break;
            }

            return entry;

        }
    }
}

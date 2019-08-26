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

namespace VCProj2json
{
    class ProgramOptions
    {
        [Value(0, MetaName = "projectfilepath", Required = true)]
        public string ProjectFilePath { get; set; }

        [Option("buildtarget", Default = "Release|Win32", HelpText = "Configuration and Platform. e.g. Release|Win32")]
        public string BuildTarget { get; set; }

        [Option('h', "skipheader", Required = false, HelpText ="Skip header file.")]
        public bool SkipHeader { get; set; } = false;

        [Option('u', "utf8", Required = false, HelpText = "Convert file encoding to UTF8withBOM.")]
        public bool ConvertToUtf8 { get; set; } = false;

        [Option('p',"printplatforms", Required = false, HelpText = "Print installed platforms.")]
        public bool PrintPlatforms { get; set; } = false;
    }
}

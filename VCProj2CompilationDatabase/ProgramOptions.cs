using CommandLine;
using System;

namespace VCProj2json
{
    class ProgramOptions
    {
        [Value(0, MetaName = "projectfilepath", Required = true)]
        public string ProjectFilePath { get; set; }

        [Option("buildtarget", Default = "Release|Win32", HelpText = "Configuration and Platform. e.g. Release|Win32")]
        public string BuildTarget { get; set; }

        [Option('i', "includesheader", Required = false, Default = false, HelpText = "Include header files.")]
        public bool IncludesHeaderFiles { get; set; }

        [Option('u', "utf8", Required = false, Default = false, HelpText = "Only Convert file encoding to UTF8withBOM.")]
        public bool OnlyConvertToUtf8 { get; set; }

        [Option('a', "autoutf8", Required = false, Default = false, HelpText = "Convert file encoding to UTF8withBOM on the fly.")]
        public bool ConvertToUtf8OnTheFly { get; set; }

        [Option("printplatforms", Required = false, Default = false, HelpText = "Print installed platforms.")]
        public bool PrintPlatforms { get; set; }

        [Option('w', "nowwarnaserror", Required = false, Default = false)]
        public bool NowWarnAsError { get; set; }

        [Option("printclangcommand", Required = false, Default = false)]
        public bool PrintClangCommand { get; set; }
    }
}

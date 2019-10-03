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
    public class FileCompilationInfo : ICloneable
    {
        public string command { get; set; }
        public string file { get; set; }
        public string directory { get; set; }
        public List<string> arguments { get; set; }
        public string output { get; set; }

        object ICloneable.Clone() => this.Clone();

        public FileCompilationInfo Clone() => new FileCompilationInfo
        {
            arguments = this.arguments != null ? new List<string>(this.arguments) : null,
            command = this.command,
            directory = this.directory,
            file = this.file,
            output = this.output,
        };

        [System.Text.Json.Serialization.JsonIgnore]
        public bool PreferredCommand { get; set; } = true;

        static readonly List<string> DefaultArgs = new List<string>()
        {
            "clang",
            "-fms-compatibility",
            "-mms-bitfields",
            "-fvisibility-ms-compat",
            //"-fms-volatile",
            "-fdiagnostics-format=msvc",
            "-gcodeview",
            "-nobuiltininc",
            "-nostdinc ",
            "-nostdlibinc ",
            "-nostdinc++ ",
            "-Wno-pragma-pack",
            "-Wno-incompatible-ms-struct", // TODO
            "-Wno-nonportable-include-path",
            "-D_Complex=_Complex_value"

        };

        public FileCompilationInfo()
        {
        }

        public void AddArg(string arg)
        {
            if (PreferredCommand)
                arguments = null;

            if (PreferredCommand || command != null)
            {
                if (command == null)
                    command = string.Join(" ", DefaultArgs);

                command += ' ';
                command += arg;

            }
            else
            {
                if (arguments == null)
                {
                    arguments = new List<string>(DefaultArgs);
                }
                arguments.Add(arg);
            }
        }

        public override string ToString() => command ?? string.Join(" ", arguments);
    }
}

using System;
using System.Reflection;

namespace VCProj2json
{
    sealed class WCECompilerToolWrapper : VCProjectEngineLibrary.VCCLCompilerTool
    {
        VCProjectWCEPlatformLibrary.VCCLWCECompilerToolBase _CompilerTool;
        public WCECompilerToolWrapper(VCProjectWCEPlatformLibrary.VCCLWCECompilerToolBase compilerTool) => _CompilerTool = compilerTool ?? throw new ArgumentNullException();
        public string get_PropertyOption(string Prop, int dispidProp)
        {
            throw new NotImplementedException();
        }

        public string AdditionalOptions
        {
            get => _CompilerTool.AdditionalOptions;
            set => _CompilerTool.AdditionalOptions = value;
        }
        public string toolName
        {
            get => _CompilerTool.ToolName;
        }
        public VCProjectEngineLibrary.warningLevelOption WarningLevel
        {
            get => (VCProjectEngineLibrary.warningLevelOption)_CompilerTool.WarningLevel;
            set => _CompilerTool.WarningLevel = (VCProjectWCEPlatformLibrary.warningLevelOption)value;
        }
        public bool WarnAsError
        {
            get => _CompilerTool.WarnAsError;
            set => _CompilerTool.WarnAsError = value;
        }
        public bool SuppressStartupBanner
        {
            get => _CompilerTool.SuppressStartupBanner;
            set => _CompilerTool.SuppressStartupBanner = value;
        }
        public bool Detect64BitPortabilityProblems
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public VCProjectEngineLibrary.debugOption DebugInformationFormat
        {
            get => (VCProjectEngineLibrary.debugOption)_CompilerTool.DebugInformationFormat;
            set => _CompilerTool.DebugInformationFormat = (VCProjectWCEPlatformLibrary.debugDeviceOption)value;
        }
        public VCProjectEngineLibrary.compileAsManagedOptions CompileAsManaged
        {
            get => VCProjectEngineLibrary.compileAsManagedOptions.managedNotSet;
            set => throw new NotImplementedException();
        }
        public string AdditionalIncludeDirectories
        {
            get => _CompilerTool.AdditionalIncludeDirectories;
            set => _CompilerTool.AdditionalIncludeDirectories = value;
        }
        public string AdditionalUsingDirectories
        {
            get => _CompilerTool.AdditionalUsingDirectories;
            set => _CompilerTool.AdditionalUsingDirectories = value;
        }
        public VCProjectEngineLibrary.optimizeOption Optimization
        {
            get => (VCProjectEngineLibrary.optimizeOption)_CompilerTool.Optimization;
            set => _CompilerTool.Optimization = (VCProjectWCEPlatformLibrary.optimizeOption)value;
        }
        public VCProjectEngineLibrary.inlineExpansionOption InlineFunctionExpansion
        {
            get => (VCProjectEngineLibrary.inlineExpansionOption)_CompilerTool.InlineFunctionExpansion;
            set => _CompilerTool.InlineFunctionExpansion = (VCProjectWCEPlatformLibrary.inlineExpansionOption)value;
        }
        public bool EnableIntrinsicFunctions
        {
            get => _CompilerTool.EnableIntrinsicFunctions;
            set => _CompilerTool.EnableIntrinsicFunctions = value;
        }
        public VCProjectEngineLibrary.favorSizeOrSpeedOption FavorSizeOrSpeed
        {
            get => (VCProjectEngineLibrary.favorSizeOrSpeedOption)_CompilerTool.FavorSizeOrSpeed;
            set => _CompilerTool.FavorSizeOrSpeed = (VCProjectWCEPlatformLibrary.favorSizeOrSpeedOption)value;
        }
        public bool OmitFramePointers
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public bool EnableFiberSafeOptimizations
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public bool WholeProgramOptimization
        {
            get => _CompilerTool.WholeProgramOptimization;
            set => _CompilerTool.WholeProgramOptimization = value;
        }
        public string PreprocessorDefinitions
        {
            get => _CompilerTool.PreprocessorDefinitions;
            set => _CompilerTool.PreprocessorDefinitions = value;
        }
        public bool IgnoreStandardIncludePath
        {
            get => _CompilerTool.IgnoreStandardIncludePath;
            set => _CompilerTool.IgnoreStandardIncludePath = value;
        }
        public VCProjectEngineLibrary.preprocessOption GeneratePreprocessedFile
        {
            get => (VCProjectEngineLibrary.preprocessOption)_CompilerTool.GeneratePreprocessedFile;
            set => _CompilerTool.GeneratePreprocessedFile = (VCProjectWCEPlatformLibrary.preprocessOption)value;
        }
        public bool KeepComments
        {
            get => _CompilerTool.KeepComments;
            set => _CompilerTool.KeepComments = value;
        }
        public bool StringPooling
        {
            get => _CompilerTool.StringPooling;
            set => _CompilerTool.StringPooling = value;
        }
        public bool MinimalRebuild
        {
            get => _CompilerTool.MinimalRebuild;
            set => _CompilerTool.MinimalRebuild = value;
        }
        public VCProjectEngineLibrary.cppExceptionHandling ExceptionHandling
        {
            get => (VCProjectEngineLibrary.cppExceptionHandling)_CompilerTool.ExceptionHandling;
            set => _CompilerTool.ExceptionHandling = (VCProjectWCEPlatformLibrary.cppExceptionHandling)value;
        }
        public VCProjectEngineLibrary.basicRuntimeCheckOption BasicRuntimeChecks
        {
            get => VCProjectEngineLibrary.basicRuntimeCheckOption.runtimeBasicCheckNone;
            set => throw new NotImplementedException();
        }
        public bool SmallerTypeCheck
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public VCProjectEngineLibrary.runtimeLibraryOption RuntimeLibrary
        {
            get => (VCProjectEngineLibrary.runtimeLibraryOption)_CompilerTool.RuntimeLibrary;
            set => _CompilerTool.RuntimeLibrary = (VCProjectWCEPlatformLibrary.runtimeLibraryOption)value;
        }
        public VCProjectEngineLibrary.structMemberAlignOption StructMemberAlignment
        {
            get => (VCProjectEngineLibrary.structMemberAlignOption)_CompilerTool.StructMemberAlignment;
            set => _CompilerTool.StructMemberAlignment = (VCProjectWCEPlatformLibrary.structMemberAlignOption)value;
        }
        public bool BufferSecurityCheck
        {
            get => _CompilerTool.BufferSecurityCheck;
            set => _CompilerTool.BufferSecurityCheck = value;
        }
        public bool EnableFunctionLevelLinking
        {
            get => _CompilerTool.EnableFunctionLevelLinking;
            set => _CompilerTool.EnableFunctionLevelLinking = value;
        }
        public VCProjectEngineLibrary.floatingPointModel floatingPointModel
        {
            get => (VCProjectEngineLibrary.floatingPointModel)_CompilerTool.floatingPointModel;
            set => _CompilerTool.floatingPointModel = (VCProjectWCEPlatformLibrary.floatingPointModel)value;
        }
        public bool FloatingPointExceptions
        {
            get => _CompilerTool.FloatingPointExceptions;
            set => _CompilerTool.FloatingPointExceptions = value;
        }
        public bool DisableLanguageExtensions
        {
            get => _CompilerTool.DisableLanguageExtensions;
            set => _CompilerTool.DisableLanguageExtensions = value;
        }
        public bool DefaultCharIsUnsigned
        {
            get => _CompilerTool.DefaultCharIsUnsigned;
            set => _CompilerTool.DefaultCharIsUnsigned = value;
        }
        public bool TreatWChar_tAsBuiltInType
        {
            get => _CompilerTool.TreatWChar_tAsBuiltInType;
            set => _CompilerTool.TreatWChar_tAsBuiltInType = value;
        }
        public bool ForceConformanceInForLoopScope
        {
            get => _CompilerTool.ForceConformanceInForLoopScope;
            set => _CompilerTool.ForceConformanceInForLoopScope = value;
        }
        public bool RuntimeTypeInfo
        {
            get => _CompilerTool.RuntimeTypeInfo;
            set => _CompilerTool.RuntimeTypeInfo = value;
        }
        public bool OpenMP
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public VCProjectEngineLibrary.pchOption UsePrecompiledHeader
        {
            get => (VCProjectEngineLibrary.pchOption)_CompilerTool.UsePrecompiledHeader;
            set => _CompilerTool.UsePrecompiledHeader = (VCProjectWCEPlatformLibrary.pchOption)value;
        }
        public string PrecompiledHeaderThrough
        {
            get => _CompilerTool.PrecompiledHeaderThrough;
            set => _CompilerTool.PrecompiledHeaderThrough = value;
        }
        public string PrecompiledHeaderFile
        {
            get => _CompilerTool.PrecompiledHeaderFile;
            set => _CompilerTool.PrecompiledHeaderFile = value;
        }
        public bool ExpandAttributedSource
        {
            get => _CompilerTool.ExpandAttributedSource;
            set => _CompilerTool.ExpandAttributedSource = value;
        }
        public VCProjectEngineLibrary.asmListingOption AssemblerOutput
        {
            get => (VCProjectEngineLibrary.asmListingOption)_CompilerTool.AssemblerOutput;
            set => _CompilerTool.AssemblerOutput = (VCProjectWCEPlatformLibrary.asmListingOption)value;
        }
        public string AssemblerListingLocation
        {
            get => _CompilerTool.AssemblerListingLocation;
            set => _CompilerTool.AssemblerListingLocation = value;
        }
        public string ObjectFile
        {
            get => _CompilerTool.ObjectFile;
            set => _CompilerTool.ObjectFile = value;
        }
        public string ProgramDataBaseFileName
        {
            get => _CompilerTool.ProgramDataBaseFileName;
            set => _CompilerTool.ProgramDataBaseFileName = value;
        }
        public VCProjectEngineLibrary.browseInfoOption BrowseInformation
        {
            get => (VCProjectEngineLibrary.browseInfoOption)_CompilerTool.BrowseInformation;
            set => _CompilerTool.BrowseInformation = (VCProjectWCEPlatformLibrary.browseInfoOption)value;
        }
        public string BrowseInformationFile
        {
            get => _CompilerTool.BrowseInformationFile;
            set => _CompilerTool.BrowseInformationFile = value;
        }
        public VCProjectEngineLibrary.callingConventionOption CallingConvention
        {
            get => VCProjectEngineLibrary.callingConventionOption.callConventionCDecl;
            set => throw new NotImplementedException();
        }
        public VCProjectEngineLibrary.CompileAsOptions CompileAs
        {
            get => (VCProjectEngineLibrary.CompileAsOptions)_CompilerTool.CompileAs;
            set => _CompilerTool.CompileAs = (VCProjectWCEPlatformLibrary.CompileAsOptions)value;
        }
        public string DisableSpecificWarnings
        {
            get => _CompilerTool.DisableSpecificWarnings;
            set => _CompilerTool.DisableSpecificWarnings = value;
        }
        public string ForcedIncludeFiles
        {
            get => _CompilerTool.ForcedIncludeFiles;
            set => _CompilerTool.ForcedIncludeFiles = value;
        }
        public string ForcedUsingFiles
        {
            get => "";
            set => throw new NotImplementedException();
        }
        public bool ShowIncludes
        {
            get => _CompilerTool.ShowIncludes;
            set => _CompilerTool.ShowIncludes = value;
        }
        public string UndefinePreprocessorDefinitions
        {
            get => _CompilerTool.UndefinePreprocessorDefinitions;
            set => _CompilerTool.UndefinePreprocessorDefinitions = value;
        }
        public bool UndefineAllPreprocessorDefinitions
        {
            get => _CompilerTool.UndefineAllPreprocessorDefinitions;
            set => _CompilerTool.UndefineAllPreprocessorDefinitions = value;
        }
        public bool EnablePREfast
        {
            get => _CompilerTool.EnablePREfast;
            set => _CompilerTool.EnablePREfast = value;
        }
        public string ToolPath
        {
            get => _CompilerTool.ToolPath;
        }
        public string FullIncludePath
        {
            get => _CompilerTool.FullIncludePath;
        }
        public object VCProjectEngine
        {
            get => _CompilerTool.VCProjectEngine;
        }
        public bool CompileOnly
        {
            get => _CompilerTool.CompileOnly;
            set => _CompilerTool.CompileOnly = value;
        }
        public string ToolKind
        {
            get => _CompilerTool.ToolKind;
        }
        public VCProjectEngineLibrary.enhancedInstructionSetType EnableEnhancedInstructionSet
        {
            get => VCProjectEngineLibrary.enhancedInstructionSetType.enhancedInstructionSetTypeNotSet;
            set => throw new NotImplementedException();
        }
        public int ExecutionBucket
        {
            get => 0;
            set => throw new NotImplementedException();
        }
        public bool UseUnicodeResponseFiles
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public bool GenerateXMLDocumentationFiles
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public string XMLDocumentationFileName
        {
            get => "";
            set => throw new NotImplementedException();
        }
        public bool UseFullPaths
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public bool OmitDefaultLibName
        {
            get => false;
            set => throw new NotImplementedException();
        }
        public VCProjectEngineLibrary.compilerErrorReportingType ErrorReporting
        {
            get => VCProjectEngineLibrary.compilerErrorReportingType.compilerErrorReportingDefault;
            set => throw new NotImplementedException();
        }
    }
}

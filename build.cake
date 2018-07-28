#tool nuget:?package=OpenCover
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Codecov

// Consts.
const string Version = "1.0.0";
const string ProjectName = "Pocket.Common";

// Arguments.
var configuration = Argument("configuration", "Release");

// Variables.
var target =  EnvironmentVariable("target") ?? Argument("target", "Default");
var version = EnvironmentVariable("version") ?? Version;
var nugetApiKey = EnvironmentVariable("nugetApiKey") ?? Argument("nugetApiKey", "");
var codecovApiKey = EnvironmentVariable("codecovApiKey") ?? Argument("codecovApiKey", "");

// Configured paths.
var solutionRootPath = "./src";
var solutionPath = $"{solutionRootPath}/{ProjectName}.sln";
var testsPath = $"{solutionRootPath}/Tests/Pocket.Common.Tests.csproj";

// Tasks.
Task("Clean")
    .Does(() =>
    {
        CleanDirectory("./artifacts");
    });

Task("Restore-NuGet-Packages")
    .Does(() =>
    {
        DotNetCoreRestore(solutionPath);
    });

Task("Build")
    .Does(() =>
    {
        DotNetCoreBuild(solutionPath, new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            ArgumentCustomization = arg => arg.AppendSwitch("/p:DebugType", "=", "Full")
        });
    });

Task("Run-Tests")
    .Does(() =>
    {
        var dotNetTestSettings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
        };
        var coverageOutput = File("./artifacts/tests-coverage.xml");
        var openCoverSettings = new OpenCoverSettings
        { 
            OldStyle = true,
            Register = "user",
            ReturnTargetCodeOffset = 0
        }
        .WithFilter("+[*]* -[*Tests]*");

        OpenCover(context => context.DotNetCoreTest(testsPath, dotNetTestSettings), coverageOutput, openCoverSettings);
    });

Task("Upload-Coverage")
    .Does(() =>
    {
        Codecov("./artifacts/tests-coverage.xml", codecovApiKey);
    });

Task("Pack")
    .Does(() => 
    {
        CreateDirectory("./artifacts/out/lib");
        CopyDirectory(
            $"./src/Core/bin/{configuration}",
            "./artifacts/out/lib");

        NuGetPack($"./src/{ProjectName}.nuspec", new NuGetPackSettings
        {
            Version = version,
            OutputDirectory = "./artifacts",
        });
    });

Task("Publish")
    .Does(() =>
    {
        NuGetPush($"./artifacts/{ProjectName}.{version}.nupkg", new NuGetPushSettings
        {
            Source = "https://api.nuget.org/v3/index.json",
            ApiKey = nugetApiKey,
        });
    });

// Executable tasks.
Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Tests")
    .IsDependentOn("Pack");

Task("Default-CI")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Tests")
    .IsDependentOn("Upload-Coverage");

Task("Deploy")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Tests")
    .IsDependentOn("Pack")
    .IsDependentOn("Publish");

RunTarget(target);
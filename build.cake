#tool "nuget:?package=OpenCover"

const string ProjectName = "Pocket.Common";

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var name = Argument("project", ProjectName);

var solutionRootPath = "./src";
var solutionPath = $"{solutionRootPath}/{name}.sln";
var testsPath = $"{solutionRootPath}/Tests/Pocket.Common.Tests.csproj";

Task("Clean")
    .Does(() =>
    {
        CleanDirectory("./artifacts");
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore(solutionPath);
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
        DotNetCoreBuild(solutionPath, new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            ArgumentCustomization = arg => arg.AppendSwitch("/p:DebugType", "=", "Full"),
        });
    });

Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var dotNetTestSettings = new DotNetCoreTestSettings
        {
            Configuration = "Debug",
            NoBuild = false,
        };
        var coverageOutput = File("./artifacts/OpenCover.xml");
        var openCoverSettings = new OpenCoverSettings
        { 
            OldStyle = true,
            Register = "user",
        }
        .WithFilter("+[*]* -[Pocket.Common.Tests*]*");

        OpenCover(context => context.DotNetCoreTest(testsPath, dotNetTestSettings), coverageOutput, openCoverSettings);
    });

Task("Default")
    .IsDependentOn("Run-Tests");

RunTarget(target);
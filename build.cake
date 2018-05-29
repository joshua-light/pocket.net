#tool "nuget:?package=OpenCover"
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Codecov

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
        });
    });

Task("Run-Tests")
    .Does(() =>
    {
        var dotNetTestSettings = new DotNetCoreTestSettings
        {
            Configuration = "Debug",
            NoBuild = false,
        };
        var coverageOutput = File("./artifacts/tests-coverage.xml");
        var openCoverSettings = new OpenCoverSettings
        { 
            OldStyle = true,
            Register = "user",
        }
        .WithFilter("+[*]* -[*.Tests*]*");

        OpenCover(context => context.DotNetCoreTest(testsPath, dotNetTestSettings), coverageOutput, openCoverSettings);
    });

Task("Upload-Coverage")
    .Does(() =>
    {
        Codecov("./artifacts/tests-coverage.xml", "0c521d31-a979-4ed8-8c6d-6d8f7205bf3d");
    });

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Tests")
    .IsDependentOn("Upload-Coverage");

RunTarget(target);
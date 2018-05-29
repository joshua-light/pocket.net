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
        });
    });

Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCoreTool(
            testsPath,
            "xunit",
            $""
        );
    });

Task("Default")
    .IsDependentOn("Run-Tests");

RunTarget(target);
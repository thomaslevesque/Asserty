#!/usr/bin/env dotnet

#:package Bullseye@6.1.0
#:package SimpleExec@13.0.0

using static Bullseye.Targets;
using static SimpleExec.Command;

string artifactsDir = Path.GetFullPath("artifacts");
string logsDir = Path.Combine(artifactsDir, "logs");
string buildLogFile = Path.Combine(logsDir, "build.binlog");
string packagesDir = Path.Combine(artifactsDir, "packages");

string solutionFile = "Asserty.slnx";
string libraryProject = "src/Asserty/Asserty.csproj";

Target(
    "artifactDirectories",
    () =>
    {
        Directory.CreateDirectory(artifactsDir);
        Directory.CreateDirectory(logsDir);
        Directory.CreateDirectory(packagesDir);
    });

Target(
    "build",
    dependsOn: ["artifactDirectories"],
    () => Run(
        "dotnet",
        $"build -c Release /bl:\"{buildLogFile}\" \"{solutionFile}\""));

Target(
    "test",
    dependsOn: ["build"],
    () => Run(
        "dotnet",
        $"""
         test -c Release --no-build "{solutionFile}"
         """));

Target(
    "pack",
    dependsOn: ["artifactDirectories", "build"],
    () => Run(
        "dotnet",
        $"pack -c Release --no-build -o \"{packagesDir}\" \"{libraryProject}\""));

Target("default", dependsOn: ["test", "pack"]);

await RunTargetsAndExitAsync(args);

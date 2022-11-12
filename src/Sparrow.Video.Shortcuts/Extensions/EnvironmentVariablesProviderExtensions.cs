﻿using Sparrow.Video.Abstractions.Services;
using Sparrow.Video.Shortcuts.Enums;

namespace Sparrow.Video.Shortcuts.Extensions;

public static class EnvironmentVariablesProviderExtensions
{
    public static string CurrentProjectOpenMode(this IEnvironmentVariablesProvider environment)
        => environment.GetVariable(EnvironmentVariableNames.ProjectOpenMode) 
            ?? ProjectModes.New;

    public static bool IsSerialize(this IEnvironmentVariablesProvider environment, bool @default = false)
    {
        var all = System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User);
        var isSerialize = environment.GetVariable(EnvironmentVariableNames.Serialize);
        return bool.Parse(isSerialize ?? @default.ToString());
    }

    public static string OutputFileName(
        this IEnvironmentVariablesProvider environment, string @default = "[Autoshortcut]Project")
            => environment.GetVariable(EnvironmentVariableNames.OutputName) 
                ?? @default;

    public static bool IsDevelopment(this IEnvironmentVariablesProvider environment)
    {
        var variable = environment.GetVariable(EnvironmentVariableNames.Environment);
        return variable == "development" || variable == "dev";
    }

    public static string? GetInputDirectoryPath(this IEnvironmentVariablesProvider environment)
        => environment.GetVariable(EnvironmentVariableNames.InputDirectoryPath);

    public static string GetOutputVideoQuality(
        this IEnvironmentVariablesProvider environment, string @default = "HD")
            => environment.GetVariable(EnvironmentVariableNames.Quality) ?? @default;
}

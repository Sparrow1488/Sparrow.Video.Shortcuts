﻿using Newtonsoft.Json;
using Sparrow.Video.Abstractions.Primitives;
using Sparrow.Video.Abstractions.Projects;
using Sparrow.Video.Abstractions.Projects.Options;
using Sparrow.Video.Abstractions.Rules;
using Sparrow.Video.Primitives;
using Sparrow.Video.Shortcuts.Primitives.Structures;
using Sparrow.Video.Shortcuts.Rules;

namespace Sparrow.Video.Shortcuts.Projects.Options;

[Serializable]
public class ProjectOptions : IProjectOptions
{
    public ProjectOptions()
    {
        Structure = DefaultStructure;
    }

    [JsonConstructor]
    internal ProjectOptions(
        IFilesStructure structure, 
        IFileRulesContainer rulesContainer, 
        string projectName,
        IProjectRoot root)
    {
        Structure = structure;
        RulesContainer = rulesContainer;
        ProjectName = projectName;
        Root = root;
    }

    [JsonProperty]
    public IFilesStructure Structure { get; private set; }
    [JsonProperty]
    public string ProjectName { get; private set; } = $"Project_{DateTime.Now.Millisecond}";
    [JsonProperty]
    public IFileRulesContainer RulesContainer { get; } = new FileRulesContainer();
    [JsonProperty]
    public IProjectRoot Root { get; private set; }
    [JsonIgnore]
    public IFilesStructure DefaultStructure { get; } = new NameStructure();

    public IProjectOptions Named(string name)
    {
        ProjectName = name.Trim();
        return this;
    }

    public IProjectOptions SetRootDirectory(string path)
    {
        var paths = new ProjectPaths
        {
            RootPath = StringPath.Create(path).Value
        };
        Root = ShortcutProjectRoot.Default.WithPaths(paths);
        return this;
    }

    public IProjectOptions StructureBy(IFilesStructure structure)
    {
        Structure = structure;
        return this;
    }

    public IProjectOptions WithRules(Action<IFileRulesContainer> projectRules)
    {
        projectRules?.Invoke(RulesContainer);
        return this;
    }
}

﻿using Newtonsoft.Json;
using Sparrow.Video.Abstractions.Enums;
using Sparrow.Video.Abstractions.Primitives;
using Sparrow.Video.Abstractions.Rules;
using Sparrow.Video.Shortcuts.Factories;

namespace Sparrow.Video.Shortcuts.Rules;

[Serializable]
public abstract class FileRuleBase : IFileRule
{
    [JsonProperty]
    public abstract RuleName RuleName { get; }
    [JsonProperty]
    public bool IsApplied { get; set; }
    [JsonIgnore]
    public abstract Func<IProjectFile, bool> Condition { get; }
    
    /// <summary>
    ///     Default value is <see cref="RuleApply.Permanent"/>
    /// </summary>
    [JsonProperty]
    public virtual RuleApply RuleApply => RuleApply.Permanent;

    public void Applied() => IsApplied = true;
    public bool IsInRule(IProjectFile file) => Condition.Invoke(file);

    object ICloneable.Clone() => Clone();

    public virtual IFileRule Clone()
    {
        var rule = (FileRuleBase)FileRuleFactory.CreateDefaultRule(GetType());
        rule.IsApplied = IsApplied;
        return rule;
    }
}

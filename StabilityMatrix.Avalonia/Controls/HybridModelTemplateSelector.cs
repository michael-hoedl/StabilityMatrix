﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using StabilityMatrix.Core.Models;

namespace StabilityMatrix.Avalonia.Controls;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class HybridModelTemplateSelector : IDataTemplate
{
    // ReSharper disable once CollectionNeverUpdated.Global
    [Content]
    public Dictionary<HybridModelType, IDataTemplate> Templates { get; } = new();

    // Check if we can accept the provided data
    public bool Match(object? data)
    {
        return data is HybridModelFile;
    }

    // Build the DataTemplate here
    public Control Build(object? data)
    {
        if (data is not HybridModelTemplateSelector card)
            throw new ArgumentException(null, nameof(data));

        if (Templates.TryGetValue(card.Type, out var type))
        {
            return type.Build(card)!;
        }

        // Fallback to Local
        return Templates[HybridModelType.Local].Build(card)!;
    }
}

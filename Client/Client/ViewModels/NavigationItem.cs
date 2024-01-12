using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Client.ViewModels;

public class NavigationItem {
    public readonly Type ViewType;

    public NavigationItem(string label, string iconKey, Type viewType) {
        ViewType = viewType;
        Label = label;
        Application.Current!.TryFindResource(iconKey, out var iconGeometry);
        if(iconGeometry is StreamGeometry geometry)
            Icon = geometry;
        else
            throw new Exception($"Icon {iconKey} not found");
    }

    public StreamGeometry Icon { get; init; }
    public string Label { get; init; }
}
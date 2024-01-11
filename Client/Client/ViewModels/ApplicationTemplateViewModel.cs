using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class ApplicationTemplateViewModel : TemplateViewModelBase {
    [ObservableProperty] private bool _isNavMenuOpen = true;
    [ObservableProperty] private NavigationItem _selectedNavItem;

    public ObservableCollection<NavigationItem> NavItems { get; init; } = [
        new NavigationItem("Home", "Icon.Home", typeof(ApplicationView)),
        new NavigationItem("Dashboard", "Icon.LaptopRegular", typeof(DashboardView)),
        new NavigationItem("Units", "Icon.CalendarDateRegular", typeof(UnitCreateView))
    ];

    partial void OnSelectedNavItemChanged(NavigationItem? value) {
        Console.WriteLine($"Navigating to {value?.Label}");
        value?.Navigate();
    }

    [RelayCommand]
    public void ToggleNavMenuCommand() {
        IsNavMenuOpen = !IsNavMenuOpen;
    }
}

// where T is ViewBase
public class NavigationItem {
    private readonly Type _viewType;

    public NavigationItem(string label, string iconKey, Type viewType) {
        _viewType = viewType;
        Label = label;
        Application.Current!.TryFindResource(iconKey, out var iconGeometry);
        if(iconGeometry is StreamGeometry geometry)
            Icon = geometry;
        else
            throw new Exception($"Icon {iconKey} not found");
    }

    public StreamGeometry Icon { get; init; }
    public string Label { get; init; }


    public void Navigate() {
        Router.NavigateTo(_viewType);
    }
}
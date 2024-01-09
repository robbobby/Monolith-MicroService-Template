using System;
using System.Collections.ObjectModel;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class ApplicationTemplateViewModel : TemplateViewModelBase {
    [ObservableProperty] private bool _isNavMenuOpen = true;
    [ObservableProperty] private NavigationItem _selectedNavItem;

    public ObservableCollection<NavigationItem> NavItems { get; init; } = [
        new NavigationItem("Home", "Home", typeof(ApplicationView)),
        new NavigationItem("Dashboard", "Home", typeof(DashboardView))
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
public class NavigationItem(string label, string icon, Type viewType) {
    public string Icon = icon;
    public Type ViewType = viewType;
    public string Label { get; init; } = label;

    public void Navigate() {
        Router.NavigateTo(viewType);
    }
}
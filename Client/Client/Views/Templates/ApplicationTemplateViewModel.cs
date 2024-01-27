using System.Collections.ObjectModel;
using Client.Models;
using Client.Models.State;
using Client.ViewModels;
using Client.Views.Application;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ComponentLibrary;

namespace Client.Views.Templates;

public partial class ApplicationTemplateViewModel : TemplateViewModelBase, INavigationView {
    [ObservableProperty] private bool _isNavMenuOpen = true;

    public ApplicationTemplateViewModel() {
        ToggleMenuContext = new ToggleMenuModel(_isNavMenuOpen, ToggleNavMenuCommand, NavItems, this);
        AppState.Application = Avalonia.Application.Current;
    }

    private ObservableCollection<NavigationItem> NavItems { get; } = [
        new NavigationItem("Home", ResourceKeys.Icon.Home, typeof(ApplicationView)),
        new NavigationItem("Dashboard", ResourceKeys.Icon.LaptopRegular, typeof(DashboardView)),
        new NavigationItem("Organisations", ResourceKeys.Icon.CalendarRegular, typeof(OrganisationCreateView)),
        new NavigationItem("Backlog", ResourceKeys.Icon.Backlog, typeof(BacklogView))
    ];

    public ToggleMenuModel ToggleMenuContext { get; init; }

    [RelayCommand]
    private void ToggleNavMenuCommand() {
        IsNavMenuOpen = !IsNavMenuOpen;
    }
}

using System.Collections.ObjectModel;
using Client.Models;
using Client.ViewModels;
using Client.Views.Application;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Templates;

public partial class ApplicationTemplateViewModel : TemplateViewModelBase, INavigationView {
    [ObservableProperty] private bool _isNavMenuOpen = true;

    public ApplicationTemplateViewModel() {
        ToggleMenuContext = new ToggleMenuModel(_isNavMenuOpen, ToggleNavMenuCommand, NavItems, this);
    }

    private ObservableCollection<NavigationItem> NavItems { get; } = [
        new NavigationItem("Home", "Icon.Home", typeof(ApplicationView)),
        new NavigationItem("Dashboard", "Icon.LaptopRegular", typeof(DashboardView)),
        new NavigationItem("Units", "Icon.CalendarRegular", typeof(UnitCreateView)),
        new NavigationItem("Backlog", "Icon.Backlog", typeof(BacklogView))
    ];

    public ToggleMenuModel ToggleMenuContext { get; init; }

    [RelayCommand]
    private void ToggleNavMenuCommand() {
        IsNavMenuOpen = !IsNavMenuOpen;
    }
}

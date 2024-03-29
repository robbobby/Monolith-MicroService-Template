using System.Collections.ObjectModel;
using Client.Models;
using Client.ViewModels;
using Client.Views.Application;
using Client.Views.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Templates;

public partial class SettingsTemplateViewModel : TemplateViewModelBase, INavigationView {
    [ObservableProperty] private bool _isNavMenuOpen = true;

    public SettingsTemplateViewModel() {
        ToggleMenuContext = new ToggleMenuModel(_isNavMenuOpen, ToggleNavMenuCommand, NavItems, this);
    }

    private ObservableCollection<NavigationItem> NavItems { get; } = [
        new NavigationItem("Back To App", "Icon.ChevronLeft", typeof(ApplicationView)),
        new NavigationItem("User Settings", "Icon.UserRegular", typeof(UserSettingsView)),
        new NavigationItem("Organisation Settings", "Icon.OrganizationRegular", typeof(OrganisationSettingsView))
    ];

    public ToggleMenuModel ToggleMenuContext { get; init; }

    [RelayCommand]
    private void ToggleNavMenuCommand() {
        IsNavMenuOpen = !IsNavMenuOpen;
    }
}

public interface INavigationView {
    public bool IsNavMenuOpen { get; set; }
}

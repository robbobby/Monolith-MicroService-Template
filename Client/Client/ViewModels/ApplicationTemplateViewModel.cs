using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;
using Client.Models.Apis;
using Client.Views;
using Common.IdentityApi;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class ApplicationTemplateViewModel : TemplateViewModelBase {
    private readonly Router _router;
    [ObservableProperty] private bool _hasUnits = AppState.User.Units.Any();
    [ObservableProperty] private bool _isNavMenuOpen = true;
    [ObservableProperty] private NavigationItem _selectedNavItem = null!; // TODO: Fix this
    [ObservableProperty] private UserUnit? _selectedUnit;

    public ApplicationTemplateViewModel(Router router) {
        _router = router;
        SelectedUnit = Units.FirstOrDefault(u => u.Id == AppState.User.SelectedUnit?.Id);
        SubscribeToUnitChanged();
    }

    public ObservableCollection<UserUnit> Units => AppState.User.Units;

    public ObservableCollection<NavigationItem> NavItems { get; init; } = [
        new NavigationItem("Home", "Icon.Home", typeof(ApplicationView)),
        new NavigationItem("Dashboard", "Icon.LaptopRegular", typeof(DashboardView)),
        new NavigationItem("Units", "Icon.CalendarDateRegular", typeof(UnitCreateView))
    ];

    partial void OnSelectedUnitChanged(UserUnit? value) {
        if(!SelectedUnit?.Equals(value) ?? value != null) SelectedUnit = value;
    }

    private void SubscribeToUnitChanged() {
        AppState.User.Units.CollectionChanged += (_, _) => {
            HasUnits = AppState.User.Units.Any();
        };

        AppState.User.PropertyChanged += (_, _) => {
            if(SelectedUnit == null || AppState.User.SelectedUnit?.Id != SelectedUnit.Id) {
                SelectedUnit = Units.FirstOrDefault(u => u.Id == AppState.User.SelectedUnit?.Id);
                Console.WriteLine($"Setting Selected unit: {SelectedUnit?.Name}");
            }
        };
    }

    [RelayCommand]
    public async Task SignOutCommand() {
        var result = await Api.Auth.SignOut();
        if(result.Succeeded == ResultType.Success) _router.NavigateTo<LoginView>();
    }

    partial void OnSelectedNavItemChanged(NavigationItem? value) {
        _router.NavigateTo(value!.ViewType);
    }

    [RelayCommand]
    public void ToggleNavMenuCommand() {
        IsNavMenuOpen = !IsNavMenuOpen;
    }
}
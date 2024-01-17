using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Models;
using Client.Models.Apis;
using Client.Models.Apis.Http;
using Client.ViewModels;
using Client.Views.Auth;
using Client.Views.Settings;
using Common.IdentityApi;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Controls;

public partial class NavigationMenuModel : ViewModelBase {
    private readonly Router _router;
    [ObservableProperty] private bool _isNavMenuOpen;
    [ObservableProperty] private NavigationItem? _selectedNavItem;

    private ToggleMenuModel? _toggleMenuContext;

    public NavigationMenuModel(Router router, ToggleMenuModel toggleMenuContext) {
        _toggleMenuContext = toggleMenuContext;
        _router = router;
        ToggleCommand = toggleMenuContext.ToggleCommand;
        NavItems = toggleMenuContext.NavItems;
        SelectedNavItem = NavItems.FirstOrDefault(item => item.ViewType == router.ContentView.GetType());
    }

    public ICommand ToggleCommand { get; init; }
    public ObservableCollection<NavigationItem> NavItems { get; init; }

    [RelayCommand]
    public void SettingsCommand() {
        _router.NavigateTo<UserSettingsView>();
    }

    partial void OnSelectedNavItemChanged(NavigationItem? value) {
        if(value != null) _router.NavigateTo(value.ViewType);
    }

    [RelayCommand]
    public async Task SignOutCommand() {
        var result = await Api.Auth.SignOut();
        if(result.Succeeded == ResultType.Success) _router.NavigateTo<LoginView>();
    }
}
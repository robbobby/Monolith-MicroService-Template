using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Client.Models.Apis;
using Client.ViewModels;
using Client.Views.Application;
using Common.IdentityApi;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ApplicationTemplateViewModel = Client.Views.Templates.ApplicationTemplateViewModel;
using DashboardView = Client.Views.Application.DashboardView;
using LoginView = Client.Views.Auth.LoginView;
using Router = Client.Models.Router;
using UnitCreateView = Client.Views.Application.UnitCreateView;

namespace Client.Controls;

public partial class NavigationMenu : UserControl {
    public static readonly StyledProperty<ApplicationTemplateViewModel.ToggleMenuModel> ToggleMenuDataContextProperty =
        AvaloniaProperty.Register<StyledElement, ApplicationTemplateViewModel.ToggleMenuModel>(
            nameof(ToggleMenuDataContext),
            defaultBindingMode: BindingMode.TwoWay);

    public NavigationMenu() {
        InitializeComponent();
        var viewModel = App.Services.GetService<NavigationMenuViewModel>()!;
        viewModel.ToggleCommand = new RelayCommand(() => ToggleMenuDataContext?.ToggleCommand?.Execute(null));
        DataContext = viewModel;
    }

    public ApplicationTemplateViewModel.ToggleMenuModel ToggleMenuDataContext {
        get => GetValue(ToggleMenuDataContextProperty);
        set => SetValue(ToggleMenuDataContextProperty, value);
    }
}

public partial class NavigationMenuViewModel : ViewModelBase {
    private readonly Router _router;
    [ObservableProperty] private bool _isNavMenuOpen;
    [ObservableProperty] private NavigationItem? _selectedNavItem;

    public NavigationMenuViewModel(Router router) {
        _router = router;
        SelectedNavItem = NavItems.FirstOrDefault(n => n.ViewType == router.ContentView?.GetType());
    }

    public ObservableCollection<NavigationItem> NavItems { get; init; } = [
        new NavigationItem("Home", "Icon.Home", typeof(ApplicationView)),
        new NavigationItem("Dashboard", "Icon.LaptopRegular", typeof(DashboardView)),
        new NavigationItem("Units", "Icon.CalendarDateRegular", typeof(UnitCreateView))
    ];

    public ICommand ToggleCommand { get; set; }

    partial void OnSelectedNavItemChanged(NavigationItem? value) {
        if(value != null) _router.NavigateTo(value.ViewType);
    }

    [RelayCommand]
    public async Task SignOutCommand() {
        var result = await Api.Auth.SignOut();
        if(result.Succeeded == ResultType.Success) _router.NavigateTo<LoginView>();
    }
}
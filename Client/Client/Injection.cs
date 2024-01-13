using Client.Controls;
using Client.Views;
using Client.Views.Application;
using Client.Views.Templates;
using Microsoft.Extensions.DependencyInjection;
using ApplicationTemplateView = Client.Views.Templates.ApplicationTemplateView;
using ApplicationTemplateViewModel = Client.Views.Templates.ApplicationTemplateViewModel;
using AuthTemplateView = Client.Views.Templates.AuthTemplateView;
using DashboardView = Client.Views.Application.DashboardView;
using LoginView = Client.Views.Auth.LoginView;
using LoginViewModel = Client.Views.Auth.LoginViewModel;
using MainViewModel = Client.Views.MainViewModel;
using MainWindowViewModel = Client.Views.MainWindowViewModel;
using RegisterView = Client.Views.Auth.RegisterView;
using RegisterViewModel = Client.Views.Auth.RegisterViewModel;
using Router = Client.Models.Router;
using UnitCreateView = Client.Views.Application.UnitCreateView;
using UnitCreateViewModel = Client.Views.Application.UnitCreateViewModel;

namespace Client;

public class Injection {
    public static void ConfigureServices(ServiceCollection services) {
        services.AddSingleton<Router>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<LoginView>();
        services.AddSingleton<LoginViewModel>();

        services.AddSingleton<RegisterView>();
        services.AddSingleton<RegisterViewModel>();

        services.AddSingleton<AuthTemplateView>();
        services.AddSingleton<AuthTemplateViewModel>();

        services.AddSingleton<ApplicationTemplateView>();
        services.AddSingleton<ApplicationTemplateViewModel>();

        services.AddSingleton<ApplicationView>();
        services.AddSingleton<ApplicationViewModel>();

        services.AddSingleton<DashboardView>();
        services.AddSingleton<DashboardViewModel>();

        services.AddSingleton<UnitCreateView>();
        services.AddSingleton<UnitCreateViewModel>();

        services.AddSingleton<NavigationMenu>();
        services.AddSingleton<NavigationMenuViewModel>();

        services.AddSingleton<HeaderNav>();
        services.AddSingleton<HeaderNavViewModel>();
    }
}
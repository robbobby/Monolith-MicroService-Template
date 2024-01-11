using Client.ViewModels;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public class Injection {
    public static void ConfigureServices(ServiceCollection services) {
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
    }
}
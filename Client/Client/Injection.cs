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
    }
}
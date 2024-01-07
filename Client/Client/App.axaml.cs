using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public class App : Application {
    public static ServiceProvider Services { get; private set; }

    public App() {
        var services = new ServiceCollection();
        Injection.ConfigureServices(services);
        Services = services.BuildServiceProvider();
    }

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = App.Services.GetService<MainWindow>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
            singleViewPlatform.MainView = App.Services.GetService<MainView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
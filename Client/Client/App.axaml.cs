using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public class App : Application {
    public App() {
        var services = new ServiceCollection();
        Injection.ConfigureServices(services);
        Services = services.BuildServiceProvider();
    }

    public static ServiceProvider Services { get; private set; }

    public static T GetViewOrThrow<T>() where T : ViewBase {
        var view = Services.GetService<T>();
        if(view == null)
            throw new NullReferenceException(
                $"View {typeof(T).Name} not found, Did you forget to add it to Injection.cs?");
        return view;
    }
    
    public static T GetModalViewOrThrow<T>() where T : ModalViewBase {
        var view = Services.GetService<T>();
        if(view == null)
            throw new NullReferenceException(
                $"View {typeof(T).Name} not found, Did you forget to add it to Injection.cs?");
        return view;
    }

    public static ViewBase GetViewOrThrow(Type type) {
        var view = (ViewBase)Services.GetService(type)!;
        if(view == null)
            throw new NullReferenceException($"View {type.Name} not found, Did you forget to add it to Injection.cs?");
        return view;
    }

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {
        if(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = Services.GetService<MainWindow>();
        else if(ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            singleViewPlatform.MainView = Services.GetService<MainView>();

        base.OnFrameworkInitializationCompleted();
    }
}

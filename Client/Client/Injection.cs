using Avalonia.Notification;
using Client.Controls;
using Client.Models;
using Client.Models.Apis.Socket;
using Client.Service;
using Client.Views;
using Client.Views.Application;
using Client.Views.Application.Project;
using Client.Views.Auth;
using Client.Views.Settings;
using Client.Views.Templates;
using Microsoft.Extensions.DependencyInjection;
using ProjectCreateFormModel = Client.Views.Application.Project.ProjectCreateFormModel;

namespace Client;

public static class Injection {
    public static void ConfigureServices(ServiceCollection services) {
        InjectTemplates(services);
        services.AddSingleton<Router>();
        services.AddSingleton<INotificationMessageManager, NotificationMessageManager>();
        services.AddSingleton<NotificationManager>();
        services.AddSingleton<WebSocket>();

        services.AddSingleton<BacklogView>();
        services.AddSingleton<BacklogViewModel>();

        services.AddScoped<ModalService>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<LoginView>();
        services.AddSingleton<LoginViewModel>();

        services.AddSingleton<RegisterView>();
        services.AddSingleton<RegisterViewModel>();

        services.AddSingleton<ApplicationView>();
        services.AddSingleton<ApplicationViewModel>();

        services.AddSingleton<DashboardView>();
        services.AddSingleton<DashboardViewModel>();

        services.AddSingleton<UnitCreateView>();
        services.AddSingleton<UnitCreateViewModel>();

        services.AddSingleton<NavigationMenu>();
        services.AddSingleton<NavigationMenuModel>();

        services.AddSingleton<HeaderNav>();
        services.AddSingleton<HeaderNavViewModel>();

        services.AddSingleton<SettingsMainView>();
        services.AddSingleton<SettingsMainViewModel>();

        services.AddSingleton<UserSettingsView>();
        services.AddSingleton<UserSettingsViewModel>();

        services.AddSingleton<UnitSettingsView>();
        services.AddSingleton<UnitSettingsViewModel>();

        // Modals must be transient otherwise they cannot be re-attached to the visual tree
        services.AddTransient<TicketCreateForm>();
        services.AddTransient<TicketCreateFormModel>();

        services.AddTransient<ProjectCreateForm>();
        services.AddTransient<ProjectCreateFormModel>();
    }

    private static void InjectTemplates(ServiceCollection services) {
        services.AddSingleton<AuthTemplateView>();
        services.AddSingleton<AuthTemplateViewModel>();

        services.AddSingleton<ApplicationTemplateView>();
        services.AddSingleton<ApplicationTemplateViewModel>();

        services.AddSingleton<SettingsTemplateView>();
        services.AddSingleton<SettingsTemplateViewModel>();
    }
}

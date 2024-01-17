using System;
using System.ComponentModel;
using Client.Models;
using Client.ViewModels;
using Client.Views.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using ApplicationTemplateView = Client.Views.Templates.ApplicationTemplateView;
using AuthTemplateView = Client.Views.Templates.AuthTemplateView;
using LoginView = Client.Views.Auth.LoginView;
using Router = Client.Models.Router;

namespace Client.Views;

public partial class MainViewModel : ViewModelBase {
    private readonly Router _router;
    [ObservableProperty] private TemplateView _templateView;
    private ViewTemplateType _viewTemplate;

    public MainViewModel(Router router, NotificationManager notificationNotification) {
        _router = router;
        Notification = notificationNotification;
        TemplateView = App.Services.GetService<AuthTemplateView>()!;
        InitialView();

        _router.PropertyChanged += SetView;
    }

    public NotificationManager Notification { get; }

    private void InitialView() {
        _viewTemplate = ViewTemplateType.Auth;
        TemplateView.ViewModel.View = App.Services.GetService<LoginView>()!;
    }

    private void SetView(object? sender, PropertyChangedEventArgs args) {
        if(args.PropertyName != nameof(_router.ContentView)) return;

        if(_router.ContentView!.ViewTemplate != _viewTemplate) {
            _viewTemplate = _router.ContentView.ViewTemplate;
            TemplateView = _viewTemplate switch {
                ViewTemplateType.Auth => App.Services.GetService<AuthTemplateView>()!,
                ViewTemplateType.Application => App.Services.GetService<ApplicationTemplateView>()!,
                ViewTemplateType.Settings => App.Services.GetService<SettingsTemplateView>()!,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        Console.WriteLine($"Setting view to {_router.ContentView?.GetType().Name}");

        TemplateView.ViewModel.View = _router.ContentView;
    }
}

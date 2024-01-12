using System;
using System.ComponentModel;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase {
    private readonly Router _router;
    [ObservableProperty] private TemplateView _templateView;
    private ViewTemplateType _viewTemplate;

    public MainViewModel(Router router) {
        _router = router;
        TemplateView = App.Services.GetService<AuthTemplateView>()!;
        InitialView();

        _router.PropertyChanged += SetView;
    }

    private void InitialView() {
        _viewTemplate = ViewTemplateType.Auth;
        TemplateView.ViewModel.View = App.Services.GetService<LoginView>()!;
    }

    private void SetView(object? sender, PropertyChangedEventArgs args) {
        if(args.PropertyName != nameof(Router.ContentView)) return;

        if(_router.ContentView!.ViewTemplate != _viewTemplate) {
            _viewTemplate = _router.ContentView.ViewTemplate;
            TemplateView = _viewTemplate switch {
                ViewTemplateType.Auth => App.Services.GetService<AuthTemplateView>()!,
                ViewTemplateType.Application => App.Services.GetService<ApplicationTemplateView>()!,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        Console.WriteLine($"Setting view to {_router.ContentView?.GetType().Name}");

        TemplateView.ViewModel.View = _router.ContentView;
    }
}
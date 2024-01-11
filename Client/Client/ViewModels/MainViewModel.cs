using System;
using System.ComponentModel;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase {
    [ObservableProperty] private TemplateView _templateView;
    private ViewTemplateType _viewTemplate;

    public MainViewModel() {
        TemplateView = App.Services.GetService<AuthTemplateView>()!;
        InitialView();

        Router.ViewChange += SetView;
    }

    private void InitialView() {
        _viewTemplate = ViewTemplateType.Auth;
        TemplateView.ViewModel.View = App.Services.GetService<LoginView>()!;
    }

    private void SetView(object? sender, PropertyChangedEventArgs args) {
        Console.WriteLine($"The current view is {Router.ContentView.GetType().Name}");
        if(args.PropertyName != nameof(Router.ContentView)) return;

        if(Router.ContentView!.ViewTemplate != _viewTemplate) {
            _viewTemplate = Router.ContentView.ViewTemplate;
            TemplateView = _viewTemplate switch {
                ViewTemplateType.Auth => App.Services.GetService<AuthTemplateView>()!,
                ViewTemplateType.Application => App.Services.GetService<ApplicationTemplateView>()!,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        Console.WriteLine($"The current view is {Router.ContentView.GetType().Name}");
        TemplateView.ViewModel.View = Router.ContentView;
        Console.WriteLine($"The current view is {TemplateView.ViewModel.View.GetType().Name}");
    }
}
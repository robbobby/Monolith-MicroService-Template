using System;
using System.ComponentModel;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase {
    [ObservableProperty] private ViewBase? _contentViewModel;

    public MainViewModel() {
        _contentViewModel = App.Services.GetService<LoginView>()!; 
        Router.ViewChange += SetView;
    }

    private void SetView(object? sender, PropertyChangedEventArgs args) {
        if (args.PropertyName != nameof(Router.ContentView)) return;
        ContentViewModel = Router.ContentView;
    }
}
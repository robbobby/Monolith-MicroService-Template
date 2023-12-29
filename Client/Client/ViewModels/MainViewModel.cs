using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase {
    [ObservableProperty] private ViewModelBase? _contentViewModel;

    public MainViewModel() {
        _contentViewModel = LoginView;
        Router.ViewChange += SetView;
    }

    private void SetView(object? sender, PropertyChangedEventArgs args) {
        if (args.PropertyName != nameof(Router.ContentViewModel)) return;
        ContentViewModel = Router.ContentViewModel;
    }

    public LoginViewModel LoginView { get; } = new();
    public RegisterViewModel RegisterView { get; }
}
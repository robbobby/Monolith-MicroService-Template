using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class RegisterViewModel : ViewModelBase {
    
    [ObservableProperty] public string _username;
    [ObservableProperty] public string _password;
    [ObservableProperty] public string _confirmPassword;
    [ObservableProperty] public string _email;

    [RelayCommand]
    public void SwitchToLoginViewCommand() {
        Router.NavigateTo<LoginViewModel>();
    }
}
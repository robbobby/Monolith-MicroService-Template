using System;
using System.Threading.Tasks;
using Client.Views;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class LoginViewModel : ViewModelBase {
    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] [NonePersistent] private string _password = "";

    [ObservableProperty] private string _username = "";

    [RelayCommand]
    public void ForgotPasswordCommand() {
        Console.WriteLine("ForgotPasswordCommand");
    }

    [RelayCommand]
    public void SwitchToRegisterViewCommand() {
        Router.NavigateTo<RegisterView>();
    }

    [RelayCommand]
    public Task LoginCommand() {
        Router.NavigateTo<ApplicationView>();
        Console.WriteLine("LoginCommand");
        return Task.CompletedTask;
    }
    [RelayCommand]
    public Task SocialLoginCommand(LoginProvider provider) {
        switch (provider) {
            case LoginProvider.Google:
                Console.WriteLine("Google");
                break;
            case LoginProvider.Microsoft:
                Models.Api.Api.Auth.Test().ContinueWith(task => {
                    Console.WriteLine(task.Result);
                }, TaskContinuationOptions.ExecuteSynchronously);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
        }

        return Task.CompletedTask;
    }
}

public class NonePersistentAttribute : Attribute { }
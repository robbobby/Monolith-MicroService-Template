using System;
using System.Threading.Tasks;
using Client.Models.Api;
using Client.Views;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class LoginViewModel() : ViewModelBase {

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty] private string _username;
    [ObservableProperty] [NonePersistent] private string _password;

    [RelayCommand]
    public void ForgotPasswordCommand() {
        Console.WriteLine("ForgotPasswordCommand");
    }

    [RelayCommand]
    public void SwitchToRegisterViewCommand() {
        Router.NavigateTo<RegisterView>();
    }

    [RelayCommand]
    private Task LoginCommand(LoginProvider provider) {
        switch (provider) {
            case LoginProvider.Google:
                Console.WriteLine("Google");
                break;
            case LoginProvider.Microsoft:
                Apis.Identity.Test().ContinueWith(task => {
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
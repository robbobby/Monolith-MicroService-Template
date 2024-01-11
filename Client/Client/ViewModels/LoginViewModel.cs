using System;
using System.Threading.Tasks;
using Apis.Core.Model.Auth;
using Client.Models.Apis;
using Client.Views;
using Common.IdentityApi;
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
    public async Task LoginCommand() {
        try {
            IsLoading = true;
            var result = await Api.Auth.Login(new LoginRequest {
                Username = Username,
                Password = Password
            });

            if(result?.Succeeded == ResultType.Success) {
                ApiClient.SetTokens(result.Data!);
                Router.NavigateTo<ApplicationView>();
            }
        }
        finally {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public Task SocialLoginCommand(LoginProvider provider) {
        switch (provider) {
            case LoginProvider.Google:
                Console.WriteLine("Google");
                break;
            case LoginProvider.Microsoft:
                Api.Auth.Test().ContinueWith(task => {
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
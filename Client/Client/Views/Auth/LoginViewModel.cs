using System;
using System.Threading.Tasks;
using Apis.Core.Model.Auth;
using Client.Models;
using Client.Models.Apis.Http;
using Client.Models.Apis.Socket;
using Client.ViewModels;
using Client.Views.Application;
using Common.IdentityApi;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Auth;

public partial class LoginViewModel(Router _router, NotificationManager _notification, WebSocket _socket)
    : ViewModelBase {
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] [NonePersistent] private string _password = "";
    [ObservableProperty] private string _username = "";

    [RelayCommand]
    public void ForgotPasswordCommand() {
        Console.WriteLine("ForgotPasswordCommand");
    }

    [RelayCommand]
    public void SwitchToRegisterViewCommand() {
        _router.NavigateTo<RegisterView>();
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
                _router.NavigateTo<ApplicationView>();
                await _socket.Connect();
                _notification.Success("Logged in successfully");
            } else {
                _notification.Error("Failed to login");
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            _notification.Error("Failed to login");
            throw;
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
                Console.WriteLine("Microsoft");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
        }

        return Task.CompletedTask;
    }
}

public class NonePersistentAttribute : Attribute { }

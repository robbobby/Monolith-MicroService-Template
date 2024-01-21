using System;
using System.Threading.Tasks;
using Client.Models;
using Client.Models.Apis.Http;
using Client.ViewModels;
using Common.Apis.Auth;
using Common.Apis.Auth.Register;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Router = Client.Models.Router;

namespace Client.Views.Auth;

public partial class RegisterViewModel(Router _router, NotificationManager _notification) : ViewModelBase {
    [ObservableProperty] private string _confirmPassword = "";
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _firstName = "";
    [ObservableProperty] private string _lastName = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _username = "";

    [RelayCommand]
    public void SwitchToLoginViewCommand() {
        _router.NavigateTo<LoginView>();
    }

    [RelayCommand]
    public async Task RegisterCommand() {
        try {
            IsLoading = true;
            var result = await Api.Auth.Register(new RegisterRequest {
                Username = Username,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password
            });

            if(result?.Succeeded == ResultType.Success) _router.NavigateTo<LoginView>();
            else _notification.Error("Failed to register");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            _notification.Error("Failed to register");
            throw;
        }
        finally {
            IsLoading = false;
        }
    }
}

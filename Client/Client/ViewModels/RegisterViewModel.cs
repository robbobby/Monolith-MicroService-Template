using Api.Core.Model.Auth;
using Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class RegisterViewModel : ViewModelBase {
    [ObservableProperty] private string _confirmPassword = "";
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _firstName = "";
    [ObservableProperty] private string _lastName = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _username = "";

    [RelayCommand]
    public void SwitchToLoginViewCommand() {
        Router.NavigateTo<LoginView>();
    }

    [RelayCommand]
    public void RegisterCommand() {
        try {
            IsLoading = true;
            Models.Api.Api.Auth.Register(new RegisterRequest {
                Username = Username,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password
            }).ContinueWith(task => {
                if (task.IsCompletedSuccessfully) Router.NavigateTo<LoginView>();
            });
        }
        finally {
            IsLoading = false;
        }
    }
}
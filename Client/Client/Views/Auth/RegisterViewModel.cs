using System.Threading.Tasks;
using Apis.Core.Model.Auth;
using Client.Models.Apis;
using Client.ViewModels;
using Common.IdentityApi;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Router = Client.Models.Router;

namespace Client.Views.Auth;

public partial class RegisterViewModel(Router _router) : ViewModelBase {
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
        }
        finally {
            IsLoading = false;
        }
    }
}
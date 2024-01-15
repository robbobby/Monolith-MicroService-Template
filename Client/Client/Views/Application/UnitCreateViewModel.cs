using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Models;
using Client.Models.Apis;
using Client.Models.State;
using Client.ViewModels;
using Common.IdentityApi;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Application;

public partial class UnitCreateViewModel(NotificationManager _notification) : ViewModelBase {
    [ObservableProperty] private string _name = "";
    public ObservableCollection<UserOrganisation> Units => AppState.User.Units;

    [RelayCommand]
    public async Task CreateUnitCommand() {
        try {
            var result = await Api.Unit.CreateUnit(Name);
            if(result.Succeeded == ResultType.Success) {
                await Api.Auth.UpdateToken();
                _notification.Success("Unit created successfully");
            } else {
                _notification.Error("Failed to create unit");
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            _notification.Error("Failed to create unit");
        }
    }
}
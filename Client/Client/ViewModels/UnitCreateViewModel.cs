using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Models;
using Client.Models.Apis;
using Common.IdentityApi;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class UnitCreateViewModel : ViewModelBase {
    [ObservableProperty] private string _name = "";
    public ObservableCollection<UserUnit> Units => AppState.User.Units;

    [RelayCommand]
    public async Task CreateUnitCommand() {
        var result = await Api.Unit.CreateUnit(Name);
        if(result.Succeeded == ResultType.Success) Api.Auth.UpdateToken();
    }
}
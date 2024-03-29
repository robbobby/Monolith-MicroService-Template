using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Models;
using Client.Models.Apis.Http;
using Client.Models.State;
using Client.ViewModels;
using Common.Apis.Auth;
using Common.Apis.Auth.UpdateToken;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Application;

public partial class OrganisationCreateViewModel(NotificationManager _notification) : ViewModelBase {
    [ObservableProperty] private string _name = "";
    public ObservableCollection<UserOrganisation> Organisations => AppState.User.Organisations;

    [RelayCommand]
    public async Task CreateOrganisation() {
        try {
            var result = await Api.Organisations.CreateOrganisation(Name);
            if(result.Succeeded == ResultType.Success) {
                await Api.Auth.UpdateToken(new UpdateTokenRequest {
                    OrganisationId = AppState.User.SelectedOrganisation?.Id,
                    ProjectId = result.Data
                });
                _notification.Success("Organisation created successfully");
            } else {
                _notification.Error("Failed to create organisation");
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
            _notification.Error("Failed to create Organisation");
        }
    }
}

using System;
using System.Threading.Tasks;
using Client.Models;
using Client.Models.Apis.Http;
using Client.Models.State;
using Client.ViewModels;
using Common.Apis.Auth;
using Common.Apis.Auth.UpdateToken;
using Common.Apis.Project;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Application.Project;

public partial class ProjectCreateFormModel(NotificationManager _notification) : ViewModelBase {
    [ObservableProperty] private string _name = string.Empty;

    [RelayCommand]
    public async Task Create() {
        Console.WriteLine("Creating project");
        var response = await Api.Project.CreateProject(new ProjectCreateRequest {
            Name = _name
        });

        if(response == null || response.Succeeded != ResultType.Success) {
            _notification.Error("Failed to create project");
            return;
        }

        _notification.Success("Project created successfully");

        await Api.Auth.UpdateToken(new UpdateTokenRequest {
            OrganisationId = AppState.User.SelectedOrganisation?.Id,
            ProjectId = response.Data
        });
    }
}

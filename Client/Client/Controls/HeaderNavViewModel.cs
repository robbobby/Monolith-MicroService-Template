using System.Collections.ObjectModel;
using System.Linq;
using Client.Models.Apis.Http;
using Client.Models.State;
using Client.ViewModels;
using Common.Apis.Auth.UpdateToken;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Controls;

public partial class HeaderNavViewModel : ViewModelBase {
    [ObservableProperty] private UserOrganisation? _selectedOrganisation;
    [ObservableProperty] private Project? _selectedProject;

    public HeaderNavViewModel() {
        SelectedOrganisation = Organisations.FirstOrDefault(u => u.Id == AppState.User.SelectedOrganisation?.Id);
        SelectedProject = Projects.FirstOrDefault(u => u.Id == AppState.User.SelectedProject?.Id);
        SubscribeToStateChanges();
    }

    public ObservableCollection<UserOrganisation> Organisations => AppState.User.Organisations;
    public ObservableCollection<Project> Projects => AppState.User.Projects;

    partial void OnSelectedOrganisationChanged(UserOrganisation? value) {
        if(value?.Id != AppState.User.SelectedOrganisation?.Id && value != null)
            Api.Auth.UpdateToken(new UpdateTokenRequest {
                OrganisationId = value?.Id,
                ProjectId = SelectedProject?.Id
            });

        if(!SelectedOrganisation?.Equals(value) ?? value != null)
            SelectedOrganisation = value;
        if(SelectedProject != AppState.User.SelectedProject)
            SelectedProject = AppState.User.SelectedProject;
    }

    private void SubscribeToStateChanges() {
        AppState.User.PropertyChanged += (_, _) => {
            if(AppState.User.SelectedOrganisation != null)
                SelectedOrganisation =
                    Organisations.FirstOrDefault(u => u.Id == AppState.User.SelectedOrganisation?.Id);

            if(SelectedProject == null || AppState.User.SelectedProject?.Id != SelectedProject.Id)
                SelectedProject = Projects.FirstOrDefault(u => u.Id == AppState.User.SelectedProject?.Id);
        };
    }
}

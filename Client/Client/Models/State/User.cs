using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Models.State;

public partial class User : ObservableObject {
    [ObservableProperty] private Guid _id;
    [ObservableProperty] private UserOrganisation? _selectedOrganisation;
    [ObservableProperty] private Project? _selectedProject;

    public ObservableCollection<Project> Projects { get; init; } = [];
    public ObservableCollection<UserOrganisation> Organisations { get; init; } = [];

    public void FromToken(string token) {
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        Id = Guid.Parse(decodedToken.Claims.First(claim => claim.Type == CustomClaimType.UserId).Value);

        var organisationClaims = decodedToken.Claims.First(claim => claim.Type == CustomClaimType.Organisations).Value;

        if(!string.IsNullOrEmpty(organisationClaims)) {
            var organisations = JsonSerializer.Deserialize<UserOrganisation[]>(organisationClaims) ?? [];
            Organisations.Clear();

            foreach (var unit in organisations)
                if(Organisations.All(u => u.Id != unit.Id))
                    Organisations.Add(unit);
        }

        Console.WriteLine(token);

        var organisationId = decodedToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimType.OrganisationId)
            ?.Value;
        if(organisationId != null) {
            SelectedOrganisation = Organisations.FirstOrDefault(u => u.Id == Guid.Parse(organisationId));
            SetProjects(SelectedOrganisation);
            var projectId = decodedToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimType.ProjectId)?.Value;
            if(projectId != null)
                SelectedProject = SelectedOrganisation.Projects.FirstOrDefault(p => p.Id == Guid.Parse(projectId));
            else
                SelectedProject = SelectedOrganisation.Projects.FirstOrDefault();
        } else {
            SelectedOrganisation = Organisations.FirstOrDefault();
            SetProjects(SelectedOrganisation);
        }
    }

    private void SetProjects(UserOrganisation? organisation) {
        Projects.Clear();
        if(organisation != null)
            foreach (var project in organisation.Projects)
                Projects.Add(project);
    }
}

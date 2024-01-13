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
    [ObservableProperty] private UserUnit? _selectedUnit;
    public ObservableCollection<UserUnit> Units { get; init; } = [];

    public void FromToken(string token) {
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        Id = Guid.Parse(decodedToken.Claims.First(claim => claim.Type == CustomClaimType.UserId).Value);

        var unitsClaimValue = decodedToken.Claims.First(claim => claim.Type == CustomClaimType.Units).Value;

        if(!string.IsNullOrEmpty(unitsClaimValue)) {
            var units = JsonSerializer.Deserialize<ObservableCollection<UserUnit>>(unitsClaimValue) ?? [];
            foreach (var unit in units)
                if(Units.All(u => u.Id != unit.Id))
                    Units.Add(unit);

            foreach (var un in Units)
                if(units.All(u => u.Id != un.Id))
                    Units.Remove(un);
        }

        SelectedUnit = Units.FirstOrDefault();
    }
}
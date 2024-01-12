using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Models;

public partial class User : ObservableObject {
    [ObservableProperty] private Guid _id;
    [ObservableProperty] private UserUnit? _selectedUnit;

    public User() {
        Units = new ObservableCollection<UserUnit>();
    }

    public ObservableCollection<UserUnit> Units { get; init; } = new();

    public void FromToken(string token) {
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        Id = Guid.Parse(decodedToken.Claims.First(claim => claim.Type == CustomClaimType.UserId).Value);

        var unitsClaimValue = decodedToken.Claims.First(claim => claim.Type == CustomClaimType.Units).Value;

        if(!string.IsNullOrEmpty(unitsClaimValue)) {
            var units = JsonSerializer.Deserialize<ObservableCollection<UserUnit>>(unitsClaimValue) ?? [];
            foreach (var unit in units)
                if(Units.Any(u => u.Id == unit.Id)) { } else {
                    Units.Add(unit);
                }

            foreach (var un in Units)
                if(units.Any(u => u.Id == un.Id)) { } else {
                    Units.Remove(un);
                }
        }

        SelectedUnit = Units.FirstOrDefault();
    }
}
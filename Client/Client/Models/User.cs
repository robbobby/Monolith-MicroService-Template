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
    public ObservableCollection<UserUnit> Units { get; init; } = new();

    public void FromToken(string token) {
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        Id = Guid.Parse(decodedToken.Claims.First(claim => claim.Type == CustomClaimType.UserId).Value);

        var unitsClaimValue = decodedToken.Claims.First(claim => claim.Type == CustomClaimType.Units).Value;

        if(!string.IsNullOrEmpty(unitsClaimValue)) {
            var units = JsonSerializer.Deserialize<ObservableCollection<UserUnit>>(unitsClaimValue) ?? [];
            Units.Clear();
            foreach (var unit in units) Units.Add(unit);
        }
    }
}
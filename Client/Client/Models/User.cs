using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using Common.Model;

namespace Client.Models;

public class User {
    public static Guid Id { get; set; }
    public static UserUnit[] Units { get; set; } = [];

    public static void FromToken(string token) {
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        Id = Guid.Parse(decodedToken.Claims.First(claim => claim.Type == CustomClaimTypes.Id).Value);

        var unitsClaimValue = decodedToken.Claims.First(claim => claim.Type == CustomClaimTypes.Units).Value;

        Units = JsonSerializer.Deserialize<UserUnit[]>(unitsClaimValue) ?? [];

        Console.WriteLine($"User: {Id}");
    }
}
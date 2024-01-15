using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.CustomHttpContext;

public class AppAuthoriseAttribute : AuthorizeAttribute, IAuthorizationFilter {
    private readonly UserRole _requiredRole;
    private readonly bool _requireExactRole;

    public AppAuthoriseAttribute(UserRole requiredRole = UserRole.None, bool requireExactRole = false) {
        _requiredRole = requiredRole;
        _requireExactRole = requireExactRole;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {
        var httpContext = context.HttpContext;
        var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

        if(authHeader != null) {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authHeader.Replace("Bearer ", ""));
            var userIdClaim = token.Claims.First(claim => claim.Type == CustomClaimType.UserId);
            var organisationsClaim = token.Claims.Where(claim => claim.Type == CustomClaimType.Organisations)
                .Select(claim => JsonSerializer.Deserialize<UserOrganisation[]>(claim.Value)).FirstOrDefault();
            var currentOrganisationClaim = token.Claims
                .Where(claim => claim.Type == CustomClaimType.CurrentOrganisationId)
                .Select(claim => Guid.Parse(claim.Value)).FirstOrDefault();

            var userContext = new UserHttpContext {
                UserId = userIdClaim.Value,
                Organisations = organisationsClaim,
                CurrentOrganisation = currentOrganisationClaim
            };

            httpContext.SetUserContext(userContext);

            Console.WriteLine("Current org " + userContext.CurrentOrganisation);
            foreach (var userContextOrganisation in userContext.Organisations) {
                Console.WriteLine("from list " + userContextOrganisation.Id);
                Console.WriteLine($"IsEquaL {userContext.CurrentOrganisation == userContextOrganisation.Id}");
            }


            var userRole = userContext.Organisations.FirstOrDefault(o => o.Id == userContext.CurrentOrganisation);
            if(_requireExactRole) {
                if(userRole.Role != _requiredRole) context.Result = new ForbidResult();
            } else {
                if(userRole.Role < _requiredRole) {
                    Console.WriteLine(userRole.Role);
                    Console.WriteLine(_requiredRole);
                    context.Result = new ForbidResult();
                }
            }

            Console.WriteLine("Hello");
            Console.WriteLine(context.Result);
        }
    }
}
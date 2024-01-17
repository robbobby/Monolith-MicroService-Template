using Microsoft.AspNetCore.Http;

namespace Core.CustomHttpContext;

public static class HttpContextExtensions {
    private const string UserKey = "UserId";

    public static UserHttpContext UserContext(this HttpContext context) {
        return context.Items[UserKey] as UserHttpContext;
    }

    public static void SetUserContext(this HttpContext context, UserHttpContext user) {
        context.Items[UserKey] = user;
    }
}

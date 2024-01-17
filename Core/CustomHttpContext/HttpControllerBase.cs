using Microsoft.AspNetCore.Mvc;

namespace Core.CustomHttpContext;

public class HttpControllerBase : ControllerBase {
    protected new UserHttpContext User => HttpContext.UserContext();
}

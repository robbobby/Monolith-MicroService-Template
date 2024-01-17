using Common.Model;

namespace Core;

public class UserHttpContext {
    public string UserId { get; set; }
    public UserOrganisation[] Organisations { get; set; } = [];
    public Guid CurrentOrganisation { get; set; }
}

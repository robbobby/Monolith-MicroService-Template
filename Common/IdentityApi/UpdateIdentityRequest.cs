namespace Common.IdentityApi;

public class UpdateIdentityRequest {
    public string NewEmail { get; set; }
    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
}

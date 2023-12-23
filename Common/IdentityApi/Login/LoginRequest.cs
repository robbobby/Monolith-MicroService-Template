namespace Common.IdentityApi.Login;

public class LoginRequest {
    public string Email { get; set; }
    public string Password { get; set; }
    public string TwoFactoryCode { get; set; }
    public string TwoFactoryRecorveryCode { get; set; }
}
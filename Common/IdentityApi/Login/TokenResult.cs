namespace Common.IdentityApi.Login;

public class TokenResult {
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ExpiresIn { get; set; }
}
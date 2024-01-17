namespace Common.IdentityApi;

public class TwoFactoryAuthRequest {
    public bool Enable { get; set; }
    public string TwoFactoryCode { get; set; }
    public bool ResetSharedKey { get; set; }
    public bool ResetRecoveryCodes { get; set; }
    public bool ForgetMachine { get; set; }
}

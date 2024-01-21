namespace Common.Apis.Auth;

public class TwoFactorAuthDetails {
    public string SharedKey { get; set; }
    public int RecoveryCodesLeft { get; set; }
    public string[] RecoveryCodes { get; set; }
    public bool IsTwoFactoryEnabled { get; set; }
    public bool IsMachineRemembered { get; set; }
}

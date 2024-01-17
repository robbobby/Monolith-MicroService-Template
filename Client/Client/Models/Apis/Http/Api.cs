namespace Client.Models.Apis.Http;

public static class Api {
    public static AuthApi Auth { get; } = new();
    public static UnitApi Unit { get; } = new();
}
namespace Core.WebSocketMessageKeys;

public static class OrgClientToServer {
    public const string SendMessage = "SendMessage";
}

public static class OrgServerToClient {
    public const string NewMessage = "NewMessage";
    public const string ClientJoined = "ClientJoined";
}

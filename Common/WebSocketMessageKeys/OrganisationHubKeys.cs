namespace Common.WebSocketMessageKeys;

public static class OrgClientToServer {
    public const string SendMessage = "SendMessage";
}

public static class OrgServerToClient {
    public const string NewMessage = "NewMessage";
    public const string ClientJoined = "ClientJoined";
}

public class TicketServerToClient {
    public const string ClientJoined = "ClientJoined";
}

public class TicketClientToServer {
    public const string SendMessage = "SendMessage";
}

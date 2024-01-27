namespace Client.Models.Apis.Socket;

public class WebSocket {
    public OrganisationConnection OrganisationConnection { get; } = new();
    public TicketConnection TicketConnection { get; } = new();
}
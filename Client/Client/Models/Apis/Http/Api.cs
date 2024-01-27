namespace Client.Models.Apis.Http;

public static class Api {
    public static AuthApi Auth { get; } = new();
    public static OrganisationsApi Organisations { get; } = new();
    public static ProjectApi Project { get; } = new();
    public static TicketApi Ticket { get; } = new();
}

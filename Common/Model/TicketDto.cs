namespace Common.Model;

public class TicketDto {
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public TicketPriority Priority { get; set; }
    public TicketType Type { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? AssignedToId { get; set; }
    public Guid OrganisationId { get; set; }
}

public class CreateTicketRequest {
    public string? Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public TicketPriority Priority { get; set; }
    public TicketType Type { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? AssignedToId { get; set; }
    public Guid OrganisationId { get; set; }
}

public class UpdateTicketRequest {
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public TicketPriority Priority { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? AssignedToId { get; set; }
    public Guid OrganisationId { get; set; }
}

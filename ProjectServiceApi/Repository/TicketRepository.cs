using Core.Entity.Project;
using Core.RepositoryBase;

namespace ProjectServiceApi.Repository;

public class TicketRepository(RepositoryWithEntityId<TicketEntity> ticket) {
    public RepositoryWithEntityId<TicketEntity> Ticket { get; } = ticket;
    
}

using Common.Model;

namespace Core.Observers;

public class TicketNotifier
{
    public event Func<TicketDto, Task> OnTicketCreated = delegate { return Task.CompletedTask; };

    public void NotifyTicketCreated(TicketDto ticket)
    {
        OnTicketCreated.Invoke(ticket);
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Client.Models.Apis.Http;
using Common.Model;

namespace Client.Models.State;

public class AppState {
    public static User User { get; set; } = new();
    public static Application? Application { get; set; }
    public static ProjectSettings? ProjectSettings { get; set; }
    public static ProjectState ProjectState { get; } = new();
}

public class ProjectState {
    public ObservableCollection<TicketDto> Tickets { get; set; } = new();
    public async Task Refresh() {
        Tickets.Clear();
        await GetTickets();
    }

    private async Task GetTickets() {
        var result = await Api.Ticket.GetOrgTickets();
        if(result?.Data != null) {
            foreach (var ticket in result.Data) {
                Console.WriteLine(ticket);
                Tickets.Add(ticket);
            }
        }
    }

}

public class ProjectSettings {
    public List<StatusColumn> Statuses { get; set; } = new() {
        new() {
            Name = "To Do",
            Code = "Todo",
        },
        new() {
            Name = "In Progress",
            Code = "InProgress",
        },
        new() {
            Name = "Complete",
            Code = "Complete",
        }
    };
}

public class StatusColumn {
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Color { get; set; } = null!;
}

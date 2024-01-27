using System;
using Client.Models;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Views.Application.Ticket;

public partial class TicketUpdateFormModel(NotificationManager _notification) : ModalWindowModel {
    [ObservableProperty] private string _title = "hello";
    [ObservableProperty] private string _description = "hello";
    [ObservableProperty] private TicketPriority _priority = TicketPriority.Low;
    [ObservableProperty] private TicketType _type = TicketType.Bug;
    
    private TicketDto _ticket = null!;
    public TicketDto Ticket {
        get => _ticket;
        set {
            Console.WriteLine($"Ticket set to {value.Id}");
            Console.WriteLine(value.Title);
            Console.WriteLine(value.Description);
            Console.WriteLine(value.Priority);
            Console.WriteLine(value.Type);
            _ticket = value;
            Title = value.Title;
            Description = value.Description;
            Priority = value.Priority;
            Type = value.Type;
        }
    }
}

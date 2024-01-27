using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Client.Models;
using Client.Models.Apis.Http;
using Client.Models.State;
using Client.ViewModels;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Application.Ticket;

public partial class TicketCreateFormModel(NotificationManager _notification) : ViewModelBase {
    [ObservableProperty] private bool _attachToProject = true;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private ComboBoxItem _priority;
    [ObservableProperty] private TicketType _type;

    [RelayCommand]
    public async Task CreateTicket() {
        var ticket = new CreateTicketRequest {
            Title = Name,
            Description = Description,
            Status = "Todo",
            Type = Type,
            Priority = (TicketPriority)Priority.Tag!,
            ProjectId = AttachToProject ? AppState.User.SelectedProject?.Id : null,
        };

        var result = await Api.Ticket.CreateTicket(ticket);
        if(result != null && result.DidSucceed) {
            _notification.Success("Ticket created successfully");
        } else {
            _notification.Error("Failed to create ticket");
        }
    }
}

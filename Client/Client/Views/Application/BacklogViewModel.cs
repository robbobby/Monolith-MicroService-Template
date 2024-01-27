using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Client.Models.Apis.Http;
using Client.Models.State;
using Client.ViewModels;
using Client.Views.Application.Ticket;
using Common.Model;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace Client.Views.Application;

public partial class BacklogViewModel : ViewModelBase {
    public ObservableCollection<TicketDto> Tickets => AppState.ProjectState.Tickets!;
    public BacklogViewModel() {
        Console.WriteLine(AppState.ProjectState.Tickets.Count);
        ShowModal = new Interaction<ModalWindow, object?>();
    }
    
    public Interaction<ModalWindow, object?> ShowModal { get; }

    [RelayCommand]
    public async Task OpenModal() {
        var content = App.GetViewOrThrow<TicketCreateForm>();
        await ShowModal.Handle(ModalWindow.WithContent(content));
    }
}

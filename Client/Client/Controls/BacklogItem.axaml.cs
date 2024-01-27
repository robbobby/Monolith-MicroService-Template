using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Client.Models;
using Client.Views;
using Client.Views.Application.Project;
using Client.Views.Application.Ticket;
using Common.Model;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Client.Controls;

public partial class BacklogItem : UserControl {
    public Interaction<ModalWindow, object?> ShowModal { get; }

    public BacklogItem() {
        InitializeComponent();
        ShowModal = new Interaction<ModalWindow, object?>();
        TicketId.PointerPressed += ShowTicketUpdateForm;
        TicketTitle.PointerPressed += ShowTicketUpdateForm;
    }

    private void ShowTicketUpdateForm(object? sender, RoutedEventArgs e) {
        var content = App.GetModalViewOrThrow<TicketUpdateForm>();
        (content.ViewModel! as TicketUpdateFormModel)!.Ticket = (DataContext as TicketDto)!;
        Console.WriteLine((DataContext as TicketDto)!.Id);

        var view = ModalWindow.WithContent(content);
        view.ShowDialog((VisualRoot as Window)!);

        e.Handled = true;
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Client.Views.Application.Ticket;

public partial class TicketUpdateForm : ModalViewBase {
    public TicketUpdateForm(TicketUpdateFormModel viewModel) : base(viewModel) {
        InitializeComponent();
    }
    
    public TicketUpdateForm() : base(new TicketUpdateFormModel(null)) {
        InitializeComponent();
    }
}


namespace Client.Views.Application.Ticket;

public partial class TicketCreateForm : ModalViewBase, IModalContentControl {
    public TicketCreateForm(TicketCreateFormModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    public TicketCreateForm() : base(new TicketCreateFormModel(null)) {
        InitializeComponent();
        DataContext = new TicketCreateFormModel(null);
    }

    public override double ModalHeight => 400;
    public override double ModalWidth => 400;
    public override bool CanResize => false;
}

public interface IModalContentControl {
    double ModalHeight { get; }
    double ModalWidth { get; }
    bool CanResize { get; }
}

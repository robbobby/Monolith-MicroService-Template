namespace Client.Views.Application;

public partial class TicketCreateForm : ViewBase, IModalContentControl {
    public TicketCreateForm(TicketCreateFormModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public TicketCreateForm() : base(new TicketCreateFormModel()) {
        InitializeComponent();
        DataContext = new TicketCreateFormModel();
    }

    public double ModalHeight => 400;
    public double ModalWidth => 300;
    public bool CanResize => false;
}

public interface IModalContentControl {
    double ModalHeight { get; }
    double ModalWidth { get; }
    bool CanResize { get; }
}

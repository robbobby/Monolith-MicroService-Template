using Client.Views.Application.Ticket;

namespace Client.Views.Application.Project;

public partial class ProjectCreateForm : ModalViewBase, IModalContentControl {
    public ProjectCreateForm(ProjectCreateFormModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public ProjectCreateForm() : base(new ProjectCreateFormModel(null)) {
        InitializeComponent();
    }

    public double ModalHeight => 300;
    public double ModalWidth => 400;
    public bool CanResize => false;
}

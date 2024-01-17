namespace Client.Views.Application;

public partial class BacklogView : ApplicationViewBase {
    public BacklogView(BacklogViewModel viewModel) : base(viewModel) {
        InitializeComponent();
    }

    public BacklogView() : base(new BacklogViewModel()) {
        InitializeComponent();
    }
}

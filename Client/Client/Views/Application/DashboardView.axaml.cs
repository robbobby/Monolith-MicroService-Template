namespace Client.Views.Application;

public partial class DashboardView : ApplicationViewBase<DashboardViewModel> {
    public DashboardView(DashboardViewModel viewModel) : base(viewModel) {
        InitializeComponent();
    }

    public DashboardView() : base(new DashboardViewModel()) {
        InitializeComponent();
    }
}

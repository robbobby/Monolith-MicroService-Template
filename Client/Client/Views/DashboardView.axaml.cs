using Client.ViewModels;

namespace Client.Views;

public partial class DashboardView : ApplicationViewBase {
    public DashboardView(DashboardViewModel viewModel) : base(viewModel) {
        InitializeComponent();
    }

    public DashboardView() : base(new DashboardViewModel()) {
        InitializeComponent();
    }
}
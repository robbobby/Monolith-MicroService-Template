using Client.Service;

namespace Client.Views.Application;

public partial class DashboardView : ApplicationViewBase<DashboardViewModel> {
    public DashboardView(DashboardViewModel viewModel, ModalService modalService) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
        modalService.View = this;
        viewModel.ShowModal.RegisterHandler(modalService.ShowModal);
    }

    public DashboardView() : base(new DashboardViewModel()) {
        InitializeComponent();
    }
}

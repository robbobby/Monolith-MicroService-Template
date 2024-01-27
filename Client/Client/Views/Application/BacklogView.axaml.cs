using Client.Service;

namespace Client.Views.Application;

public partial class BacklogView : ApplicationViewBase<BacklogViewModel> {
    public BacklogView(BacklogViewModel viewModel, ModalService modalService) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
        modalService.View = this;
        viewModel!.ShowModal.RegisterHandler(modalService.ShowModal);
    }
    
    public BacklogView() : base(new BacklogViewModel()) {
        InitializeComponent();
    }
}

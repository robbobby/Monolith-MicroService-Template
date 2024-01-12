using Client.ViewModels;

namespace Client.Views;

public partial class ApplicationTemplateView : TemplateView {
    public ApplicationTemplateView(ApplicationTemplateViewModel viewModel) : base(viewModel) {
        InitializeComponent();
    }

    public ApplicationTemplateView() : base(new ApplicationTemplateViewModel(new Router())) {
        InitializeComponent();
    }
}
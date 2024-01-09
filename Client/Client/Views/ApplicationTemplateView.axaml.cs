using Client.ViewModels;

namespace Client.Views;

public partial class ApplicationTemplateView : TemplateView {
    public ApplicationTemplateView(TemplateViewModelBase viewModel) : base(viewModel) {
        InitializeComponent();
    }
    
    public ApplicationTemplateView() : base(new ApplicationTemplateViewModel()) {
        InitializeComponent();
    }
}
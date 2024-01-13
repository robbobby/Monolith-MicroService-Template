namespace Client.Views.Templates;

public partial class ApplicationTemplateView : TemplateView {
    public ApplicationTemplateView(ApplicationTemplateViewModel viewModel) : base(viewModel) {
        InitializeComponent();
    }

    public ApplicationTemplateView() : base(new ApplicationTemplateViewModel()) {
        InitializeComponent();
    }
}
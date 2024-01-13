namespace Client.Views.Templates;

public partial class AuthTemplateView : TemplateView {
    public AuthTemplateView(AuthTemplateViewModel viewModel) : base(viewModel) {
        InitializeComponent();
    }

    public AuthTemplateView() : base(new AuthTemplateViewModel()) {
        InitializeComponent();
    }
}
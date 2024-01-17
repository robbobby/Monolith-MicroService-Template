namespace Client.Views.Templates;

public partial class SettingsTemplateView : TemplateView {
    public SettingsTemplateView(SettingsTemplateViewModel viewModel) : base(viewModel) {
        InitializeComponent();
    }

    public SettingsTemplateView() : base(new SettingsTemplateViewModel()) {
        InitializeComponent();
    }
}

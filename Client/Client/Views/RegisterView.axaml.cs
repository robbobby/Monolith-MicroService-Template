using Client.ViewModels;

namespace Client.Views;

public partial class RegisterView : ViewBase {
    public RegisterView(RegisterViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    public RegisterView() : base(new RegisterViewModel()) {
        InitializeComponent();
    }
}

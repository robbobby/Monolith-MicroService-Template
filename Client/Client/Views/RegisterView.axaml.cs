using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public partial class RegisterView : ViewBase {
    public RegisterView(RegisterViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    private RegisterView() : base(null!) {
        InitializeComponent();
    }
}

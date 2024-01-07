using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public partial class LoginView : ViewBase {
    public LoginView(LoginViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public LoginView() : base(null!) {
        InitializeComponent();
    }
    
}


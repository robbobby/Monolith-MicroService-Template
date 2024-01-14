using Client.Models;
using Router = Client.Models.Router;

namespace Client.Views.Auth;

public partial class LoginView : ViewBase {
    public LoginView(LoginViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public LoginView() : base(new LoginViewModel(new Router(), new NotificationManager())) {
        InitializeComponent();
    }
}
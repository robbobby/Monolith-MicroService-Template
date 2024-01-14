using Client.Models;
using Router = Client.Models.Router;

namespace Client.Views.Auth;

public partial class RegisterView : ViewBase {
    public RegisterView(RegisterViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public RegisterView() : base(new RegisterViewModel(new Router(), new NotificationManager())) {
        InitializeComponent();
    }
}
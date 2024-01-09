using Client.ViewModels;

namespace Client.Views;

public partial class MainView : ViewBase {
    public MainView(MainViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    private MainView() : base(null!) {
        InitializeComponent();
    }
}
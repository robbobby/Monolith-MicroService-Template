using Client.ViewModels;

namespace Client.Views;

public partial class UnitCreateView : ApplicationViewBase {
    public UnitCreateView(UnitCreateViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public UnitCreateView() : base(new UnitCreateViewModel()) {
        InitializeComponent();
    }
}
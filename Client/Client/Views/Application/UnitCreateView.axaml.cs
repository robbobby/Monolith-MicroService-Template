using Client.Models;

namespace Client.Views.Application;

public partial class UnitCreateView : ApplicationViewBase {
    public UnitCreateView(UnitCreateViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public UnitCreateView() : base(new UnitCreateViewModel(new NotificationManager())) {
        InitializeComponent();
    }
}

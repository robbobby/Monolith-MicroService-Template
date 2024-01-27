using Client.Models;

namespace Client.Views.Application;

public partial class OrganisationCreateView : ApplicationViewBase<OrganisationCreateViewModel> {
    public OrganisationCreateView(OrganisationCreateViewModel viewModel) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
    }

    public OrganisationCreateView() : base(new OrganisationCreateViewModel(new NotificationManager())) {
        InitializeComponent();
    }
}

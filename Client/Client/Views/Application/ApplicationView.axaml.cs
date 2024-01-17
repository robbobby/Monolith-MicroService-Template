using Avalonia.Notification;
using Client.Models;

namespace Client.Views.Application;

public partial class ApplicationView : ApplicationViewBase {
    public ApplicationView(ApplicationViewModel viewModel) : base(viewModel) {
        ViewTemplate = ViewTemplateType.Application;
        InitializeComponent();
        DataContext = viewModel;
    }

    public ApplicationView() : base(new ApplicationViewModel(new NotificationMessageManager(),
        new NotificationManager())) {
        ViewTemplate = ViewTemplateType.Application;
        InitializeComponent();
    }
}

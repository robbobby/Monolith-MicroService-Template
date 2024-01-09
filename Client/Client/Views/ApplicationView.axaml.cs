using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Client.ViewModels;

namespace Client.Views;

public partial class ApplicationView : ApplicationViewBase {
    public ApplicationView(ApplicationViewModel viewModel) : base(viewModel) {
        ViewTemplate = ViewTemplateType.Application;
        InitializeComponent();
        DataContext = viewModel;
    }
    
    public ApplicationView() : base(new ApplicationViewModel()) {
        ViewTemplate = ViewTemplateType.Application;
        InitializeComponent();
    }
}
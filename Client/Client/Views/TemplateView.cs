using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public class TemplateView : UserControl {
    public TemplateView(TemplateViewModelBase viewModel) {
        ViewModel = viewModel;
        DataContext = viewModel;
    }

    public TemplateViewModelBase ViewModel { get; }
}
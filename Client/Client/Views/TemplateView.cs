using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public class TemplateView : UserControl {
    private readonly TemplateViewModelBase _viewModel;
    public TemplateView(TemplateViewModelBase viewModel) {
        _viewModel = viewModel;
        DataContext = viewModel;
    }
    public TemplateViewModelBase ViewModel => _viewModel;
}
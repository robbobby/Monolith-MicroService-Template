using Avalonia.Controls;
using Client.Views.Application;

namespace Client.Views;

public partial class ModalWindow : Window {
    public ModalWindow(ModalWindowModel viewModel, IModalContentControl? content = null) {
        if(content != null) {
            Width = content.ModalWidth;
            Height = content.ModalHeight;
            CanResize = content.CanResize;
        }

        DataContext = viewModel;

        InitializeComponent();
    }
}

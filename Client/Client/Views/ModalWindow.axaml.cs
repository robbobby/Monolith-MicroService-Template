using Avalonia.Controls;
using Client.Views.Application;
using Client.Views.Application.Project;
using Client.Views.Application.Ticket;

namespace Client.Views;

public partial class ModalWindow : Window {
    private ModalWindow(ModalViewBase? content = null) {
        var viewModel = new ModalWindowModel {
            ModalContent = content
        };
        if(content != null) {
            Width = content.ModalWidth;
            Height = content.ModalHeight;
            CanResize = content.CanResize;
        }

        DataContext = viewModel;
        InitializeComponent();
    }

    public static ModalWindow WithContent(ModalViewBase content) {
        return new(content);
    }
}

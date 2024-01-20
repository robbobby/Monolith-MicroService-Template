using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using ReactiveUI;

namespace Client.Views.Application;

public partial class BacklogView : ApplicationViewBase<BacklogViewModel> {
  
    public BacklogView(BacklogViewModel viewModel, ModalService modalService) : base(viewModel) {
        InitializeComponent();
        DataContext = viewModel;
        modalService.View = this;
        viewModel!.ShowModal.RegisterHandler(modalService.ShowModal);
    }
    
    public BacklogView() : base(new BacklogViewModel()) {
        InitializeComponent();
    }
}

public class ModalService {
    public ViewBase View { get; set; }

    public async Task ShowModal<TWindow>(InteractionContext<TWindow, object?> interaction) 
        where TWindow : Window {
        var dialog = interaction.Input;

        if (dialog != null) {
            var result = await dialog.ShowDialog<object>(View.GetVisualRoot() as Window);
            interaction.SetOutput(result);
        }      
    }
}
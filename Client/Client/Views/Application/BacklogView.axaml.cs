using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using ReactiveUI;

namespace Client.Views.Application;

public partial class BacklogView : ApplicationViewBase<BacklogViewModel> {
    private readonly IModalService _modalService;
  
    public BacklogView(BacklogViewModel viewModel, IModalService modalService) : base(viewModel) {
        InitializeComponent();
        _modalService = modalService;
        DataContext = viewModel;
        viewModel!.ShowModal.RegisterHandler(DoShowDialogAsync);
    }
    
    public BacklogView(IModalService modalService) : base(new BacklogViewModel()) {
        InitializeComponent();
        _modalService = modalService;
    }

    private async Task DoShowDialogAsync(InteractionContext<ModalWindow,
                                             object?> interaction) {
        await _modalService.ShowDialog(interaction, this.GetVisualRoot() as Window);
    }
}

public interface IModalService {
    Task ShowDialog<TWindow>(InteractionContext<TWindow, object?> interaction, Window parentWindow)
        where TWindow : Window;
}

public class ModalService : IModalService {
    public async Task ShowDialog<TWindow>(InteractionContext<TWindow, object?> interaction, Window parentWindow) 
        where TWindow : Window {
        var dialog = interaction.Input;

        if (dialog != null) {
            var result = await dialog.ShowDialog<object>(parentWindow);
            interaction.SetOutput(result);
        }      
    }
}
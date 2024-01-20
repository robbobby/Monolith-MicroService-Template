using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using ReactiveUI;

namespace Client.Views.Application;

public partial class BacklogView : ApplicationViewBase {
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
        var dialog = interaction.Input;
        var result = await _modalService.ShowDialog<ModalWindow?>(dialog, this.GetVisualRoot() as Window);
        interaction.SetOutput(result);
    }
}

public interface IModalService {
    Task<object> ShowDialog<TWindow>(TWindow dialog, Window parentWindow) 
        where TWindow : Window;
}

public class ModalService : IModalService {
    public async Task<object> ShowDialog<TWindow>(TWindow dialog, Window parentWindow) 
        where TWindow : Window {
        object result = null;
        
        if (dialog != null) {
            result = await dialog.ShowDialog<object>(parentWindow);
        }
        
        return result;
    }
}
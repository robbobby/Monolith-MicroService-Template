using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Client.Views;
using ReactiveUI;

namespace Client.Service;

public class ModalService {
    public ViewBase View { get; set; }

    public async Task ShowModal<TWindow>(InteractionContext<TWindow, object?> interaction)
        where TWindow : Window {
        var dialog = interaction.Input;

        if(dialog != null) {
            var result = await dialog.ShowDialog<object>(View.GetVisualRoot() as Window);
            interaction.SetOutput(result);
        }
    }
}

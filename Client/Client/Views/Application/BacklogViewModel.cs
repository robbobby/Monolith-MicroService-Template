using System.Reactive.Linq;
using System.Threading.Tasks;
using Client.ViewModels;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace Client.Views.Application;

public partial class BacklogViewModel : ViewModelBase {
    public BacklogViewModel() {
        ShowModal = new Interaction<ModalWindow, object?>();
    }

    public Interaction<ModalWindow, object?> ShowModal { get; }

    [RelayCommand]
    public async Task OpenModal() {
        var content = App.GetViewOrThrow<TicketCreateForm>();
        var viewModel = new ModalWindowModel {
            ModalContent = content
        };

        var view = new ModalWindow(viewModel, content);
        await ShowModal.Handle(view);
    }
}

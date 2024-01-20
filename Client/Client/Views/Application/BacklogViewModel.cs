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
        var viewModel = new ModalWindowModel {
            ModalContent = App.GetViewOrThrow<ApplicationView>()
        };

        var view = new ModalWindow {
            CanResize = false,
            Width = 300,
            Height = 200,
            DataContext = viewModel
        };
        await ShowModal.Handle(view);
    }
}

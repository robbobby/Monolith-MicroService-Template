using System.Reactive.Linq;
using System.Threading.Tasks;
using Client.ViewModels;
using Client.Views.Application.Project;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace Client.Views.Application;

public partial class DashboardViewModel : ViewModelBase {
    public DashboardViewModel() {
        ShowModal = new Interaction<ModalWindow, object?>();
    }

    public Interaction<ModalWindow, object?> ShowModal { get; }

    [RelayCommand]
    private async Task OpenNewProjectForm() {
        var content = App.GetViewOrThrow<ProjectCreateForm>();
        var viewModel = new ModalWindowModel {
            ModalContent = content
        };

        var view = new ModalWindow(viewModel, content);
        await ShowModal.Handle(view);
    }
}

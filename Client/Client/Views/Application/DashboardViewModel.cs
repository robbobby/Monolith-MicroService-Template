using System.Reactive.Linq;
using System.Threading.Tasks;
using Client.ViewModels;
using Client.Views.Application.Project;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace Client.Views.Application;

public partial class DashboardViewModel : ViewModelBase {
    public Interaction<ModalWindow, object?> ShowModal { get; }

    public DashboardViewModel() {
        ShowModal = new Interaction<ModalWindow, object?>();
    }


    [RelayCommand]
    private async Task OpenNewProjectForm() {
        var content = App.GetViewOrThrow<ProjectCreateForm>();
        await ShowModal.Handle(ModalWindow.WithContent(content));
    }
}

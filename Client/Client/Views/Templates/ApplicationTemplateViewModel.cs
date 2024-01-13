using System;
using System.ComponentModel;
using System.Windows.Input;
using Client.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Views.Templates;

public partial class ApplicationTemplateViewModel : TemplateViewModelBase, INotifyPropertyChanged {
    [ObservableProperty] private bool _isNavMenuOpen = true;
    [ObservableProperty] private ApplicationTemplateViewModel _viewModelContext;

    public ApplicationTemplateViewModel() {
        ToggleMenuContext = new ToggleMenuModel(_isNavMenuOpen, ToggleNavMenuCommand, this);
    }


    public ToggleMenuModel ToggleMenuContext { get; init; }

    [RelayCommand]
    private void ToggleNavMenuCommand() {
        IsNavMenuOpen = !IsNavMenuOpen;
    }

    public partial class ToggleMenuModel : ObservableObject {
        [ObservableProperty] private bool _isNavMenuOpen;

        /// <inheritdoc />
        public ToggleMenuModel(bool isNavMenuOpen, Action toggleNavMenuCommand, INotifyPropertyChanged parent) {
            _isNavMenuOpen = isNavMenuOpen;
            ToggleCommand = new RelayCommand(toggleNavMenuCommand);
            parent.PropertyChanged += (_, args) => {
                if(args.PropertyName == nameof(IsNavMenuOpen))
                    IsNavMenuOpen = ((ApplicationTemplateViewModel)parent).IsNavMenuOpen;
            };
        }

        public ICommand ToggleCommand { get; set; }
    }
}
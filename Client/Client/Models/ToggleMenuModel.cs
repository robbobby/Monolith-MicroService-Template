using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Client.ViewModels;
using Client.Views.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.Models;

public partial class ToggleMenuModel : ObservableObject {
    [ObservableProperty] private bool _isNavMenuOpen;

    public ToggleMenuModel(bool isNavMenuOpen,
                           Action toggleNavMenuCommand,
                           ObservableCollection<NavigationItem> navItems,
                           INotifyPropertyChanged parent) {
        _isNavMenuOpen = isNavMenuOpen;
        NavItems = navItems;
        ToggleCommand = new RelayCommand(toggleNavMenuCommand);
        parent.PropertyChanged += (_, args) => {
            if(args.PropertyName == nameof(IsNavMenuOpen))
                IsNavMenuOpen = ((INavigationView)parent).IsNavMenuOpen;
        };
    }

    public ObservableCollection<NavigationItem> NavItems { get; init; }

    public ICommand ToggleCommand { get; set; }
}
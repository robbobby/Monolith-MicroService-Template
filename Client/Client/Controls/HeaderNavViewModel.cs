using System;
using System.Collections.ObjectModel;
using System.Linq;
using Client.Models.State;
using Client.ViewModels;
using Common.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Controls;

public partial class HeaderNavViewModel : ViewModelBase {
    [ObservableProperty] private bool _hasUnits = AppState.User.Units.Any();
    [ObservableProperty] private UserUnit? _selectedUnit;

    public HeaderNavViewModel() {
        SelectedUnit = Units.FirstOrDefault(u => u.Id == AppState.User.SelectedUnit?.Id);
    }

    public ObservableCollection<UserUnit> Units => AppState.User.Units;

    partial void OnSelectedUnitChanged(UserUnit? value) {
        SubscribeToUnitChanged();
        if(!SelectedUnit?.Equals(value) ?? value != null) SelectedUnit = value;
    }

    private void SubscribeToUnitChanged() {
        AppState.User.Units.CollectionChanged += (_, _) => {
            HasUnits = AppState.User.Units.Any();
        };

        AppState.User.PropertyChanged += (_, _) => {
            if(SelectedUnit == null || AppState.User.SelectedUnit?.Id != SelectedUnit.Id) {
                SelectedUnit = Units.FirstOrDefault(u => u.Id == AppState.User.SelectedUnit?.Id);
                Console.WriteLine($"Setting Selected unit: {SelectedUnit?.Name}");
            }
        };
    }
}
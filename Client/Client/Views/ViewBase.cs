using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public class ViewBase(ViewModelBase viewModelBase) : UserControl {
    public virtual bool PersistData => true;
    public ViewModelBase ViewModel => viewModelBase;
}
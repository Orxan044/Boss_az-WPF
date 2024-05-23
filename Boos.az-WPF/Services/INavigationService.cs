using Boos.az_WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace UserPanel.Services.Navigation;

public interface INavigationService
{
    void Navigate<TView, TViewModel>() where TView : Page where TViewModel : ViewModel;

}

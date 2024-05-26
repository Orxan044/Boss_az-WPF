using AdminPanel.ViewModels;
using System.Windows.Controls;

namespace Admin_Panel.Services;

public interface INavigationService
{
    void Navigate<TView, TViewModel>() where TView : Page where TViewModel : ViewModel;
}

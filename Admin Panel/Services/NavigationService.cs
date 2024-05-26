using AdminPanel.ViewModels;
using System.Windows.Controls;

namespace Admin_Panel.Services;
using AdminPanel;

public class NavigationService : INavigationService
{
    public void Navigate<TView, TViewModel>() where TView : Page where TViewModel : ViewModel
    {
        var mainVm = App.Current.MainWindow.DataContext as MainViewModel;
        if (App.Current.MainWindow.DataContext as MainViewModel is not null)
        {
            mainVm!.CurrentPage = App.Container.GetInstance<TView>();
            mainVm.CurrentPage.DataContext = App.Container.GetInstance<TViewModel>();

        }
    }
}

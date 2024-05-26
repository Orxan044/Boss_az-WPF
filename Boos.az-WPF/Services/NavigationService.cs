using Boos.az_WPF;
using Boos.az_WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace UserPanel.Services.Navigation;

public class NavigationService : INavigationService
{
    public void Navigate<TView, TViewModel>(Page CurrentPageSelect) where TView : Page where TViewModel : ViewModel 
    {
        var mainVm = App.Current.MainWindow.DataContext as MainViewModel;
        if (App.Current.MainWindow.DataContext as MainViewModel is not null)
        {
            if (CurrentPageSelect == mainVm!.CurrentPage)
            {
                mainVm!.CurrentPage = App.Container.GetInstance<TView>();
                mainVm.CurrentPage.DataContext = App.Container.GetInstance<TViewModel>();
            }
            else if(CurrentPageSelect == mainVm!.CurrentPage2)
            {
                mainVm!.CurrentPage2 = App.Container.GetInstance<TView>();
                mainVm.CurrentPage2.DataContext = App.Container.GetInstance<TViewModel>();
            }
        }
    }
}

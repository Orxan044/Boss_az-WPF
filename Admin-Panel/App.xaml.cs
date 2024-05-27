using Admin_Panel.ViewModels;
using Admin_Panel.Views;
using SimpleInjector;
using System.Windows;

namespace Admin_Panel;

public partial class App : Application
{
    public static Container Container { get; set; } = new();
    public App()
    {
        AddViews();
        AddViewModels();
    }

    //private void AddOtherServices()
    //{
    //    Container.RegisterSingleton<INavigationService, NavigationService>();
    //}

    private void AddViewModels()
    {
        Container.RegisterSingleton<MainViewModel>();
    }

    private void AddViews()
    {
        Container.RegisterSingleton<MainView>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainView = Container.GetInstance<MainView>();
        mainView.DataContext = Container.GetInstance<MainViewModel>();
        mainView.Show();
        base.OnStartup(e);
    }

}

using Admin_Panel.Services;
using Admin_Panel.Views;
using AdminPanel.ViewModels;
using SimpleInjector;
using System.Windows;

namespace AdminPanel;



public partial class App : Application
{
    public static Container Container { get; set; } = new();
    public App()
    {
        AddOtherServices();
        AddViews();
        AddViewModels();
    }

    private void AddOtherServices()
    {
        Container.RegisterSingleton<INavigationService, NavigationService>();
    }

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

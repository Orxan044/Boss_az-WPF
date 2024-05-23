using Boos.az_WPF.ViewModels;
using Boos.az_WPF.Views;
using System.Windows;
using UserPanel.Services.Navigation;
using SimpleInjector;
using Boos.az_WPF.Data;

namespace Boos.az_WPF;


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
        Container.RegisterSingleton<CategoryDbContext>();
        Container.RegisterSingleton<INavigationService, NavigationService>();
    }

    private void AddViewModels()
    {
        Container.RegisterSingleton<MainViewModel>();
        Container.RegisterSingleton<CategoryViewModel>();
    }

    private void AddViews()
    {
        Container.RegisterSingleton<MainView>();
        Container.RegisterSingleton<CategoryView>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainView = Container.GetInstance<MainView>();
        mainView.DataContext = Container.GetInstance<MainViewModel>();
        mainView.Show();
        base.OnStartup(e);
    }
}

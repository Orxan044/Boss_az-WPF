using Boos.az_WPF.ViewModels;
using Boos.az_WPF.Views;
using System.Windows;
using UserPanel.Services.Navigation;
using SimpleInjector;
using Boos.az_WPF.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Boos.az_WPF.Models;

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
        Container.RegisterSingleton<JopAnnouncementDbContext>();
        Container.RegisterSingleton<CvAnnouncementDbContext>();
        Container.RegisterSingleton<INavigationService, NavigationService>();
    }

    private void AddViewModels()
    {
        Container.RegisterSingleton<MainViewModel>();
        Container.RegisterSingleton<CategoryViewModel>();
        Container.RegisterSingleton<AllViewModel>();
        Container.RegisterSingleton<AddJopAnnouncementModel>();
        Container.RegisterSingleton<TypeAllViewModel>();
        Container.RegisterSingleton<SelectedAnnouncementViewModel>();
        Container.RegisterSingleton<SearchViewModel>();
        Container.RegisterSingleton<AllCvViewModel>();
        Container.RegisterSingleton<AddCvAnnouncementViewModel>();
        Container.RegisterSingleton<TypeCvAllViewModel>();
        Container.RegisterSingleton<CvSelectedViewModel>();
    }

    private void AddViews()
    {
        Container.RegisterSingleton<MainView>();
        Container.RegisterSingleton<CategoryView>();
        Container.RegisterSingleton<AllView>();
        Container.RegisterSingleton<AddJopAnnouncement>();
        Container.RegisterSingleton<TypeAllView>();
        Container.RegisterSingleton<SelectedAnnouncementView>();
        Container.RegisterSingleton<SearchView>();
        Container.RegisterSingleton<AllCvView>();
        Container.RegisterSingleton<AddCvAnnouncementView>();
        Container.RegisterSingleton<CvTypeAllView>();
        Container.RegisterSingleton<CvSelected>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainView = Container.GetInstance<MainView>();
        mainView.DataContext = Container.GetInstance<MainViewModel>();
        mainView.Show();
        base.OnStartup(e);
    }
}

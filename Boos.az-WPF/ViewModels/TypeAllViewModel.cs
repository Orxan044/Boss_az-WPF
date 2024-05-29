using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class TypeAllViewModel : ViewModel
{

    public double WindowSize { get; set; }
    public JopAnnouncementDbContext JopDbContext { get; set; }  
    protected readonly INavigationService NavigationService;
    public RelayCommand? MoreClickCommand { get; set; }
    public TypeAllViewModel(JopAnnouncementDbContext jopDbContext,INavigationService navigationService)
    {
        JopDbContext = jopDbContext;
        NavigationService = navigationService;
        WindowSize = jopDbContext.JopAnnouncements!.Count * 201;
        MoreClickCommand = new RelayCommand(MoreClick);
    }

    public void MoreClick(object? id)
    {
        MainViewModel mainViewModel = new MainViewModel(NavigationService);
        NavigationService.Navigate<SelectedAnnouncementView, SelectedAnnouncementViewModel>(mainViewModel.CurrentPage2!);
        if (id is not null)
        {
            var announcement = JopDbContext.GetJopAnnouncement(id.ToString()!);
            if (announcement is not null)
            {               
                var MainVm = App.Current.MainWindow.DataContext as MainViewModel;
                if (MainVm is not null)
                {
                    var NewVm = MainVm.CurrentPage2!.DataContext as SelectedAnnouncementViewModel;
                    NewVm!.SelectedAnnouncement = announcement;
                }
            }
        }
    }
}

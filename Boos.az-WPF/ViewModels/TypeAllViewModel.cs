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
    public JopAnnouncement SelectedAnnouncement { get; set; } = new();
    public RelayCommand? ItemDoubleClickCommand {get; set; }
    public TypeAllViewModel(JopAnnouncementDbContext jopDbContext,INavigationService navigationService)
    {
        JopDbContext = jopDbContext;
        NavigationService = navigationService;
        WindowSize = jopDbContext.JopAnnouncements!.Count * 200;
        ItemDoubleClickCommand = new RelayCommand(ItemDoubleClick);
    }

    public void ItemDoubleClick(object? obj)
    {
        MainViewModel mainViewModel = new(NavigationService);
        NavigationService.Navigate<SelectedAnnouncementView, SelectedAnnouncementViewModel>(mainViewModel.CurrentPage2!);

    }
}

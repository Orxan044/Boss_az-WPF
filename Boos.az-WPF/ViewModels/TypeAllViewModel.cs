using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class TypeAllViewModel : ViewModel , INotifyPropertyChanged
{

    private ObservableCollection<JopAnnouncement>? jopAnnouncements;
    public double WindowSize { get; set; }
    public JopAnnouncementDbContext JopDbContext { get; set; }
    public ObservableCollection<JopAnnouncement>? JopAnnouncements { get => jopAnnouncements; set { jopAnnouncements = value; OnPropertyChanged(); } }

    protected readonly INavigationService NavigationService;
    public RelayCommand? MoreClickCommand { get; set; }
    public TypeAllViewModel(JopAnnouncementDbContext jopDbContext,INavigationService navigationService)
    {
        JopDbContext = jopDbContext;
        NavigationService = navigationService;
        WindowSize = jopDbContext.JopAnnouncements!.Count * 300;
        MoreClickCommand = new RelayCommand(MoreClick);
        JopAnnouncements = jopDbContext.JopAnnouncementsSearch; 
    }

    public void MoreClick(object? id)
    {
        MainViewModel mainViewModel = new MainViewModel(NavigationService);
        //NavigationService.Navigate<PremiumAnnouncementView, PremiumAnnouncementViewModel>(mainViewModel.CurrentPage!);
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
                    if (NewVm!.SelectedAnnouncement.AnnouncementType == AnnouncementType.New)
                        NavigationService.Navigate<PremiumAnnouncementView, PremiumAnnouncementViewModel>(mainViewModel.CurrentPage!);
                    else
                        NavigationService.Navigate<ReklamView, ReklamViewModel>(mainViewModel.CurrentPage!);
                }
            }
        }
    }

    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------
}

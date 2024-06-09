using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class AllViewModel : ViewModel , INotifyPropertyChanged
{

    public JopAnnouncementDbContext JopAnnouncementDbContext { get; set; }
    public JopAnnouncement JopAnnouncement{ get; set; }

    public RelayCommand NewAllSeeCommand { get; set; }
    public RelayCommand NewAllSeePremiumCommand { get; set; }
    public RelayCommand GetAnnouncementCommand { get; set; }

    protected readonly INavigationService NavigationService;  

    public AllViewModel(JopAnnouncementDbContext jopAnnouncementDbContext,INavigationService navigationService)
    {
        JopAnnouncementDbContext = jopAnnouncementDbContext;
        NavigationService = navigationService;
        NewAllSeeCommand = new RelayCommand(NewAllSeeClick);
        NewAllSeePremiumCommand = new RelayCommand(NewAllSeePremiumClick);
        GetAnnouncementCommand = new RelayCommand(GetAnnouncement);
    }

    private void NewAllSeePremiumClick(object? obj)
    {
        MainViewModel mainViewModel = new(NavigationService);
        JopAnnouncementDbContext.JopAnnouncementsSearch!.Clear();
        foreach (var item in JopAnnouncementDbContext.JopAnnouncementsPerimum!)
        {
            JopAnnouncementDbContext.JopAnnouncementsSearch!.Add(item);
        }
        NavigationService.Navigate<TypeAllView, TypeAllViewModel>(mainViewModel.CurrentPage2!);
    }

    private void GetAnnouncement(object? obj)
    {
        var selectedAnnouncement = obj as JopAnnouncement;
        if (selectedAnnouncement is not null)
        {
            
            MainViewModel mainViewModel = new(NavigationService);
            NavigationService.Navigate<SelectedAnnouncementView, SelectedAnnouncementViewModel>(mainViewModel.CurrentPage2!);
            var MainVm = App.Current.MainWindow.DataContext as MainViewModel;
            if (MainVm is not null)
            {
                var NewVm = MainVm.CurrentPage2!.DataContext as SelectedAnnouncementViewModel;
                NewVm!.SelectedAnnouncement = selectedAnnouncement;
                if (NewVm!.SelectedAnnouncement.AnnouncementType == AnnouncementType.New)
                    NavigationService.Navigate<PremiumAnnouncementView, PremiumAnnouncementViewModel>(mainViewModel.CurrentPage!);
                else
                    NavigationService.Navigate<ReklamView, ReklamViewModel>(mainViewModel.CurrentPage!);
            }

        }
    }

    private void NewAllSeeClick(object? obj)
    {
        MainViewModel mainViewModel = new(NavigationService);
        JopAnnouncementDbContext.JopAnnouncementsSearch!.Clear();
        foreach (var item in JopAnnouncementDbContext.JopAnnouncements!)
        {
            JopAnnouncementDbContext.JopAnnouncementsSearch!.Add(item);
        }
        NavigationService.Navigate<TypeAllView, TypeAllViewModel>(mainViewModel.CurrentPage2!);
    }


    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------

}

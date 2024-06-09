using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.Windows.Navigation;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class AllCvViewModel : ViewModel
{

    public CvAnnouncementDbContext CvDbContext { get; set; }
    public CvAnnouncement CvAnnouncement { get; set; }

    public RelayCommand NewAllSeeCommand { get; set; }
    public RelayCommand NewAllSeePremiumCommand { get; set; }
    public RelayCommand GetAnnouncementCommand { get; set; }
    protected readonly INavigationService NavigationService;
    public AllCvViewModel(CvAnnouncementDbContext cvDbContext , INavigationService navigationService)
    {
        CvDbContext = cvDbContext;
        NavigationService = navigationService;
        NewAllSeeCommand = new RelayCommand(NewAllSeeClick);
        NewAllSeePremiumCommand = new RelayCommand(NewAllSeePremiumClick);
        GetAnnouncementCommand = new RelayCommand(GetAnnouncement);
    }

    private void NewAllSeePremiumClick(object? obj)
    {
        MainViewModel mainViewModel = new(NavigationService);
        CvDbContext.CvAnnouncementsSearch!.Clear();
        foreach (var item in CvDbContext.CvAnnouncementsPerimum!)
        {
            CvDbContext.CvAnnouncementsSearch!.Add(item);
        }
        NavigationService.Navigate<CvTypeAllView, TypeCvAllViewModel>(mainViewModel.CurrentPage2!);
    }

    private void GetAnnouncement(object? obj)
    {
        var selectedAnnouncement = obj as CvAnnouncement;
        if (selectedAnnouncement is not null)
        {

            MainViewModel mainViewModel = new(NavigationService);
            NavigationService.Navigate<CvSelected, CvSelectedViewModel>(mainViewModel.CurrentPage2!);
            var MainVm = App.Current.MainWindow.DataContext as MainViewModel;
            if (MainVm is not null)
            {
                var NewVm = MainVm.CurrentPage2!.DataContext as CvSelectedViewModel;
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
        CvDbContext.CvAnnouncementsSearch!.Clear();
        foreach (var item in CvDbContext.CvAnnouncements!)
        {
            CvDbContext.CvAnnouncementsSearch!.Add(item);
        }
        NavigationService.Navigate<CvTypeAllView, TypeCvAllViewModel>(mainViewModel.CurrentPage2!);
    }
}

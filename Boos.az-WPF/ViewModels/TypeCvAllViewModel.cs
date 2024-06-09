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

public class TypeCvAllViewModel : ViewModel , INotifyPropertyChanged
{

    public double WindowSize { get; set; }
    public CvAnnouncementDbContext CvDbContext { get; set; }
    private ObservableCollection<CvAnnouncement>? cvAnnouncements;
    public ObservableCollection<CvAnnouncement>? CvAnnouncements { get => cvAnnouncements; set { cvAnnouncements = value; OnPropertyChanged(); } }

    protected readonly INavigationService NavigationService;

    public RelayCommand? MoreClickCommand { get; set; }

    public TypeCvAllViewModel(CvAnnouncementDbContext cvDbContext, INavigationService navigationService)
    {
        CvDbContext = cvDbContext;
        NavigationService = navigationService;
        WindowSize = cvDbContext.CvAnnouncements!.Count * 300;
        MoreClickCommand = new RelayCommand(MoreClick);
        CvAnnouncements = cvDbContext.CvAnnouncementsSearch;
    }

    private void MoreClick(object? id)
    {
        MainViewModel mainViewModel = new MainViewModel(NavigationService);
        //NavigationService.Navigate<PremiumAnnouncementView, PremiumAnnouncementViewModel>(mainViewModel.CurrentPage!);
        NavigationService.Navigate<CvSelected, CvSelectedViewModel>(mainViewModel.CurrentPage2!);
        if (id is not null)
        {
            var announcement = CvDbContext.GetCvAnnouncement(id.ToString()!);
            if (announcement is not null)
            {
                var MainVm = App.Current.MainWindow.DataContext as MainViewModel;
                if (MainVm is not null)
                {
                    var NewVm = MainVm.CurrentPage2!.DataContext as CvSelectedViewModel;
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

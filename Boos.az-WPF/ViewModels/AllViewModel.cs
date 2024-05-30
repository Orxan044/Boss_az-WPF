using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class AllViewModel : ViewModel , INotifyPropertyChanged
{
    private string? latestAnnouncement = "SON İŞ ELANLARI";

    public string? LatestAnnouncement { get => latestAnnouncement; set { latestAnnouncement = value!; OnPropertyChanged(); } }

    private string? latestAnnouncementPeriumum = "PREMİUM İŞ ELANLARI";

    public string? LatestAnnouncementPeriumum { get => latestAnnouncementPeriumum; set { latestAnnouncementPeriumum = value!; OnPropertyChanged(); } }

    public JopAnnouncementDbContext JopAnnouncementDbContext { get; set; }
    public JopAnnouncement JopAnnouncement{ get; set; }

    public RelayCommand NewAllSeeCommand { get; set; }
    public RelayCommand GetAnnouncementCommand { get; set; }

    protected readonly INavigationService NavigationService;  

    //public ObservableCollection<string> JopAnnouncementNew9 { get; set; };
    public AllViewModel(JopAnnouncementDbContext jopAnnouncementDbContext,INavigationService navigationService)
    {
        JopAnnouncementDbContext = jopAnnouncementDbContext;
        NavigationService = navigationService;
        NewAllSeeCommand = new RelayCommand(NewAllSeeClick);
        GetAnnouncementCommand = new RelayCommand(GetGetAnnouncement);
    }

    private void GetGetAnnouncement(object? obj)
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
        //ypeAllViewModel typeAllViewModel = new(JopAnnouncementDbContext, NavigationService,JopAnnouncementDbContext.JopAnnouncements!);
        //typeAllViewModel.JopAnnouncements = JopAnnouncementDbContext.JopAnnouncements!;
    }


    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------

}

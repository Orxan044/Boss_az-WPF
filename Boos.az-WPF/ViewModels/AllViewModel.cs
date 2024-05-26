using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Boos.az_WPF.ViewModels;

public class AllViewModel : ViewModel , INotifyPropertyChanged
{
    private string? latestAnnouncement = "SON İŞ ELANLARI";

    public string? LatestAnnouncement { get => latestAnnouncement; set { latestAnnouncement = value!; OnPropertyChanged(); } }

    private string? latestAnnouncementPeriumum = "PREMİUM İŞ ELANLARI";

    public string? LatestAnnouncementPeriumum { get => latestAnnouncementPeriumum; set { latestAnnouncementPeriumum = value!; OnPropertyChanged(); } }

    public JopAnnouncementDbContext JopAnnouncementDbContext { get; set; }

    //public ObservableCollection<string> JopAnnouncementNew9 { get; set; };
    public AllViewModel(JopAnnouncementDbContext jopAnnouncementDbContext)
    {
        JopAnnouncementDbContext = jopAnnouncementDbContext;
    }


    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------

}

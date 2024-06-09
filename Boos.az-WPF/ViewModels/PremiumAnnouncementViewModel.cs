using Boos.az_WPF.Command;
using Boos.az_WPF.Data;

namespace Boos.az_WPF.ViewModels;

public class PremiumAnnouncementViewModel : ViewModel
{
    public RelayCommand GetPremiumCommand { get; set; }
    public JopAnnouncementDbContext JopDbContext { get; set; }
    public CvAnnouncementDbContext CvDbContext { get; set; }
    public PremiumAnnouncementViewModel(CvAnnouncementDbContext cvDbContext,JopAnnouncementDbContext jopDbContext)
    {
        GetPremiumCommand = new RelayCommand(GetPremiumClick);
        CvDbContext = cvDbContext;
        JopDbContext = jopDbContext;
    }

    private void GetPremiumClick(object? obj)
    {
        var MainVm = App.Current.MainWindow.DataContext as MainViewModel;
        if (MainViewModel.Check == false)
        {   
            var NewVm = MainVm!.CurrentPage2!.DataContext as SelectedAnnouncementViewModel;
            NewVm!.SelectedAnnouncement.AnnouncementType = Enum_Data.AnnouncementType.PREMİUM;
            JopDbContext.SaveChanges();
        }
        else
        {
            var NewVm = MainVm!.CurrentPage2!.DataContext as CvSelectedViewModel;
            NewVm!.SelectedAnnouncement.AnnouncementType = Enum_Data.AnnouncementType.PREMİUM;
            CvDbContext.SaveChanges();
        }
    }
}

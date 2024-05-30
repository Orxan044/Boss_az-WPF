using Boos.az_WPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Boos.az_WPF.ViewModels;

public class SelectedAnnouncementViewModel : ViewModel , INotifyPropertyChanged
{

    public double WindowSize { get; set; }
    public double GridSize { get; set; }

    private JopAnnouncement selectedAnnouncement;
    public JopAnnouncement SelectedAnnouncement { get => selectedAnnouncement; set { selectedAnnouncement = value; OnPropertyChanged(); } }
    public TypeAllViewModel TypeAllViewModel { get; set; }
    public SelectedAnnouncementViewModel(TypeAllViewModel typeAllViewModel)
    {
        TypeAllViewModel = typeAllViewModel;
        WindowSize = 850;
        GridSize = WindowSize - 270;
    }





    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------

}

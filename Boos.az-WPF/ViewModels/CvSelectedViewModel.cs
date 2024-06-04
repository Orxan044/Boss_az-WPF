using Boos.az_WPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Boos.az_WPF.ViewModels;

public class CvSelectedViewModel : ViewModel , INotifyPropertyChanged
{
    public double WindowSize { get; set; }
    public double GridSize { get; set; }

    private CvAnnouncement selectedAnnouncement;
    public CvAnnouncement SelectedAnnouncement { get => selectedAnnouncement; set { selectedAnnouncement = value; OnPropertyChanged(); } }

    public TypeCvAllViewModel TypeCvAllViewModel { get; set; }

    public CvSelectedViewModel(TypeCvAllViewModel typeCvAllViewModel)
    {
        TypeCvAllViewModel = typeCvAllViewModel;
        WindowSize = 850;
        GridSize = WindowSize - 270;       
    }


    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------

}

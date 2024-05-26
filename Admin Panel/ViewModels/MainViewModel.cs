using Admin_Panel.Command;
using Admin_Panel.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace AdminPanel.ViewModels;

public class MainViewModel : ViewModel , INotifyPropertyChanged
{
    public RelayCommand CloseWindow { get; set; }
    private Page currentPage;
    public Page? CurrentPage
    {
        get => currentPage;
        set { currentPage = value!; OnPropertyChanged(); }
    }

    private readonly INavigationService NavigationService;
    public MainViewModel()
    {
        CloseWindow = new RelayCommand(execute: obj => App.Current.MainWindow.Close());

        //-------------------------------------------------
        //currentPage = App.Container.GetInstance<DriverPageView>();
        //currentPage.DataContext = App.MainContainer.GetInstance<DrivePageViewModel>();
        //-------------------------------------------------
    }





    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------
}

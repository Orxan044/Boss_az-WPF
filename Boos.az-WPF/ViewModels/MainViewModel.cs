using Boos.az_WPF.Command;
using Boos.az_WPF.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class MainViewModel : ViewModel , INotifyPropertyChanged
{

    #region RelayCommand
    public RelayCommand BossAzCommand {  get; set; }
    public RelayCommand JopAnnouncementCommand { get; set; }
    public RelayCommand AddAnnouncementCommand { get; set; }
    public RelayCommand CvAnnouncementCommand { get; set; }
    #endregion


    #region ButtonBackground

    private Brush _buttonBackgroundJop;
    public Brush ButtonBackgroundJop
    {
        get { return _buttonBackgroundJop; }
        set
        {
            _buttonBackgroundJop = value;
            OnPropertyChanged();
        }
    }

    private Brush _buttonBackgroundCv;
    public Brush ButtonBackgroundCv
    {
        get { return _buttonBackgroundCv; }
        set
        {
            _buttonBackgroundCv = value;
            OnPropertyChanged();
        }
    }
    #endregion


    #region Page's
    private Page currentPage;
    private Page currentPage2;

    public Page? CurrentPage
    {
        get => currentPage;
        set { currentPage = value!; OnPropertyChanged(); }
    }

    public Page? CurrentPage2
    {
        get => currentPage2;
        set { currentPage2 = value!; OnPropertyChanged(); }
    }

    #endregion


    public static bool Check { get; set; }
    public AllViewModel AllViewModel { get; set; }
    private readonly INavigationService navigationService;

    public MainViewModel(INavigationService navigationService)
    {
        BossAzCommand = new RelayCommand(BossAzClick);
        JopAnnouncementCommand = new RelayCommand(JopAnnouncementClick);
        AddAnnouncementCommand = new RelayCommand(AddAnnouncementClick);
        CvAnnouncementCommand = new RelayCommand(CvAnnouncementClick);

        ButtonBackgroundJop = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9d54b"));
        ButtonBackgroundCv = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));

        //-------------------------------------------------
        currentPage = App.Container.GetInstance<CategoryView>();
        currentPage.DataContext = App.Container.GetInstance<CategoryViewModel>();
        //-------------------------------------------------

        //-------------------------------------------------
        currentPage2 = App.Container.GetInstance<AllView>();
        currentPage2.DataContext = App.Container.GetInstance<AllViewModel>();
        //-------------------------------------------------

        this.navigationService = navigationService;
    }

    private void CvAnnouncementClick(object? obj)
    {
        navigationService.Navigate<CategoryView, CategoryViewModel>(CurrentPage!);
        navigationService.Navigate<AllCvView, AllCvViewModel>(CurrentPage2!);
        Check = true;
        ButtonBackgroundCv = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9d54b")); 
        ButtonBackgroundJop = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF")); 
    }


    private void JopAnnouncementClick(object? obj)
    {
        navigationService.Navigate<CategoryView,CategoryViewModel>(CurrentPage!);
        navigationService.Navigate<AllView,AllViewModel>(CurrentPage2!);
        Check = false;
        ButtonBackgroundJop = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9d54b"));
        ButtonBackgroundCv= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));
    }
    public void AddAnnouncementClick(object? obj)
    {
        if(Check == false)  
            navigationService.Navigate<AddJopAnnouncement,AddJopAnnouncementModel>(CurrentPage2!);         
        else      
            navigationService.Navigate<AddCvAnnouncementView,AddCvAnnouncementViewModel>(CurrentPage2!);     
    }

    private void BossAzClick(object? obj)
    {
        navigationService.Navigate<CategoryView,CategoryViewModel>(CurrentPage!);
        navigationService.Navigate<AllView,AllViewModel>(CurrentPage2!);
        ButtonBackgroundJop = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9d54b"));
        ButtonBackgroundCv = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));
    }



    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------

}

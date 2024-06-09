using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class AddCvAnnouncementViewModel : ViewModel , INotifyPropertyChanged
{

    public string? CvRule { get; set; } =
        "1.CV/rezümelərin yerləşdirilməsi pulsuzdur. Hər bir adam 30 gün müddətində üç fərqli mövqe üçün üç rezüme yerləşdirə bilər.\r\n\n" +
        "2.CV/rezümenin ən qısa müddətdə dərc olunması üçün formanın doldurulmasına dair bütün təlimatlara ciddi riayət olunmalıdır. Səliqəsiz doldurulmuş formalar redaktəyə məruz qalacaq və dərhal dərc olunmayacaq.\r\n\n" +
        "3.Elanların baş (BÖYÜK) hərflərlə tərtib olunması, o cümlədən latın hərfləri ilə yazılması qadağandır. Elanın mətni bütünlüklə bir dildə olmalıdır.\r\n\r\n\n" +
        "4.Aşağıdakı tərkibli rezümelərin yerləşdirilməsi qadağandır:\r\n\r\nbilərəkdən həqiqətə uyğun olmayan şəxsi məlumat;\r\nədəbsiz, təhqiredici sözlər və ifadələr;\r\nreklam xarakterli;\r\nxidmət təklifləri.\r\n\n" +
        "5.CV/rezümedə şəkil varsa, aşağıdakı şərtlərə cavab verməlidir:\r\n\r\nşəkildə yalnız bir adam olmalıdır;\r\nnamizədin üzü aydın görünməlidir;\r\nfiltrlərsiz olmalıdır." +
        "6.CV/rezüme yerləşdirilərkən, «Vəzifə» sütununda kateqoriyaya uyğun olan ikili mövqe qeyd oluna bilər. Məsələn, satıcı-kassir, barista-ofisiant, sürücü-ekspeditor, çörəkçi-şirniyyatçı və s.\r\n\n" +
        "7.«Təhsil» sütununda namizədin bitirdiyi təhsil müəssisəsinin, yiyələndiyi ixtisasın adı və təhsil aldığı vaxt qeyd olunmalıdır.\r\n\n" +
        "8.«İş təcrübəsi» sütununda namizədin iş yeri, vəzifəsi və çalışdığı müddət qeyd olunmalıdır.\r\n\n" +
        "9.«Bacarıqlar» sütununda namizədin peşəkar bacarıqlarının, bildiyi dillərin, kompyuter proqramlarının və s. qeyd olunması tövsiyə olunur.\r\n\n" +
        "10.«Özünüz haqqında» adlı sütunda namizədin şəxsi xüsusiyyətləri, hobbisi, maraqları və s. qeyd olunur.\r\n\n" +
        "11.Dərc olunmuş elanda əlaqə nömrələrinin, CV/rezümenin adının dəyişdirilməsi qadağandır.\r\n\n" +
        "12.Bir CV/rezümenin yerləşdirmə vaxtından 30 gün ərzində silinmiş elanın bərpası və ya yeni elanın yerləşdirilməsi qadağandır.\r\n\n";

    #region Because of ComBoBox 
    private Category? _selectedCategory;
    private Education _selectedEducation;
    private WorkExperience _selectedWorkExperience;
    private string? _selectedGender;
    private string? imageSelected;
    private CvAnnouncement? cvAnnouncement;

    public ObservableCollection<Category> Categories { get; set; }
    public ObservableCollection<string> Elements { get; set; }
    public ObservableCollection<Education> EducationOptions { get; }
    public ObservableCollection<WorkExperience> WorkExperiences { get; }
    public ObservableCollection<string> Genders { get; set; }

    public Category SelectedCategory
    {
        get { return _selectedCategory!; }
        set
        {
            if (_selectedCategory != value)
            {
                _selectedCategory = value;
                OnPropertyChanged();
                LoadElements();
            }
        }
    }
    public string? SelectedElement { get; set; }
    public Education SelectedEducation
    {
        get { return _selectedEducation; }
        set
        {
            if (_selectedEducation != value)
            {
                _selectedEducation = value;
                OnPropertyChanged();
            }
        }
    }
    public WorkExperience SelectedWorkExperience
    {
        get { return _selectedWorkExperience; }
        set
        {
            if (_selectedWorkExperience != value)
            {
                _selectedWorkExperience = value;
                OnPropertyChanged();
            }
        }
    }
    public string SelectedGender
    {
        get { return _selectedGender!; }
        set
        {
            if (_selectedGender != value)
            {
                _selectedGender = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion

    public RelayCommand AddImageCommand { get; set; }
    public RelayCommand AddCvCommand { get; set; }
    public RelayCommand GetJopAnnouncementCommand { get; set; }
    public CategoryDbContext CategoryDbContext { get; set; }
    public CvAnnouncementDbContext CvDbContex {  get; set; }
    public string? ImageSelected { get => imageSelected; set {imageSelected = value; OnPropertyChanged(); } }
    public CvAnnouncement CvAnnouncement { get => cvAnnouncement!; set{ cvAnnouncement = value; OnPropertyChanged(); } }
    private readonly INavigationService navigationService;

    public AddCvAnnouncementViewModel(INavigationService navigationService,CategoryDbContext categoryDbContext,CvAnnouncementDbContext cvDbcontext)
    {
        #region Because of Combobox
        Categories = categoryDbContext.Categories!;
        Elements = new ObservableCollection<string>();
        EducationOptions = new ObservableCollection<Education>((Enum.GetValues(typeof(Education)) as Education[])!);
        WorkExperiences = new ObservableCollection<WorkExperience>((Enum.GetValues(typeof(WorkExperience)) as WorkExperience[])!);
        Genders = new ObservableCollection<string> { "Vacib_deyil", "Kişi", "Qadın"};
        #endregion

        CategoryDbContext = categoryDbContext;
        CvDbContex = cvDbcontext;
        this.navigationService = navigationService;

        AddImageCommand = new RelayCommand(AddImageClick);
        AddCvCommand = new RelayCommand(AddCvClick);
        GetJopAnnouncementCommand = new RelayCommand(GetJopAnnouncementClik);
        CvAnnouncement = new();
    }

    private void GetJopAnnouncementClik(object? obj)
    {
        MainViewModel mainViewModel = new(navigationService);
        navigationService.Navigate<AddJopAnnouncement, AddJopAnnouncementModel>(mainViewModel.CurrentPage2!);
    }

    private void AddCvClick(object? obj)
    {
        CvAnnouncement.SelectedCategory = new Dictionary<string, string>
        {
            {SelectedCategory.Name!,SelectedElement! }
        };
        CvAnnouncement.WorkExperience = SelectedWorkExperience;
        CvAnnouncement.Education = SelectedEducation;
        if (ImageSelected is not null) CvAnnouncement.Image = ImageSelected;

        //---------- Add -------------------------
        CvDbContex.CvAnnouncements!.Add(CvAnnouncement);
        CvDbContex.SaveChanges();
        CvAnnouncement = new();
       // notifier.ShowSuccess("CV Announcement Added Successfully");
    }

    private void AddImageClick(object? obj)
    {
        Microsoft.Win32.OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
        openFileDialog.FilterIndex = 2;
        if (openFileDialog.ShowDialog() == true)
        {
            Uri uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
            BitmapImage bitmapImage = new BitmapImage(uri);
            ImageSelected = bitmapImage.ToString();
        }
    }



    #region Because of ComBoBox
    private void LoadElements()
    {

        Elements.Clear();
        if (SelectedCategory != null && SelectedCategory.Elements != null)
            foreach (var element in SelectedCategory.Elements)
                Elements.Add(element);
    }


    #endregion




    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------

    #region Create notifier
    ToastNotifications.Notifier notifier = new ToastNotifications.Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.MainWindow,
            corner: Corner.TopLeft,
            offsetX: 5,
            offsetY: 5);

        cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(2),
            maximumNotificationCount: MaximumNotificationCount.FromCount(2));

        cfg.Dispatcher = Application.Current.Dispatcher;
    });
    #endregion
}

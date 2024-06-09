using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class AddJopAnnouncementModel : ViewModel , INotifyPropertyChanged
{


    #region Create notifier
    ToastNotifications.Notifier? notifier = new ToastNotifications.Notifier(cfg =>
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

    public string JopRule { get; set; } =
        "1.Bir vakansiyanın 30 gün müddətinə yerləşdirilməsi pulsuz həyata keçirilir.\r\n\n" +
        "2.Vakansiya yalnız Azərbaycan daxilində olan işləri əhatə etməlidir.\r\n\n" +
        "3.Vakansiya haqqında elanın ən qısa müddətdə dərc olunması üçün formanın doldurulmasına dair bütün təlimatlara ciddi riayət olunmalıdır. Səliqəsiz doldurulmuş formalar redaktəyə məruz qalacaq və dərhal dərc olunmayacaq.\r\n\n" +
        "4.Elanların yalnız baş (BÖYÜK) hərflərlə və ya başqa əlifba ilə (translitlə) yazılması qadağandır. Elan bütünlüklə bir dildə olmalıdır.\r\n\n" +
        "5.Şirkətin adı olan sütunda şirkətin rəsmi, hüquqi adı, həmin müəssisə holdinq tərkibindədirsə, holdinqin adı və şirkətin fəaliyyət istiqaməti (növü) qeyd olunmalıdır.\r\n\n" +
        "6.Əlaqələr yazılan sütunda aktiv şəhər telefonlarının nömrələri və şirkətin korporativ elektron ünvanları qeyd olunmalıdır.\r\n\n" +
        "7.İstifadəçi 5 və 6-cı bəndlərə riayət etmədikdə, elan ödənişli əsaslarla qəbul edilir.\r\n\n" +
        "8.«Əmək haqqı» sütununun doldurulması mütləqdir, məbləğ AZN-lə göstərilməlidir. Əgər əmək haqqı 500 AZN-ə qədərdirsə, əmək haqqı diapazonu 200 AZN-i; 1000 AZN-ə qədərdirsə 300 AZN-i; 2000 AZN-ə qədərdirsə, 500 AZN-i aşmamalıdır.\r\n\n" +
        "9.Dərc olunmuş elanda əlaqə nömrələrinin, vakansiyanın adının dəyişdirilməsi qadağandır.\r\n\n" +
        "10.«Namizədə olan tələblər» mümkün qədər ətraflı yazılmalıdır.\r\n\n" +
        "11.«Mövqenin (vəzifənin) təsviri» də həmçinin iş qrafiki, vəzifə öhdəlikləri və işin şərtləri qeyd olunmaqla, ətraflı yazılmalıdır.\r\n\n" +
        "12.Mövqe (vəzifə) seçilmiş kateqoriyaya uyğun olmalı, əgər elə kateqoriya yoxdursa, onda «Müxtəlif» adlanan alt-kateqoriyada yerləşdirilməlidir.\r\n\n" +
        "13.Aşağıdakı kimi elanlar dərhal ləğv olunacaq:\r\n\r\nədəbsiz, təhqiredici sözlər və ifadələr olan;\r\nşəbəkə marketinqi və ya qadağan olunmuş, şübhəli fəaliyyət növləri ilə məşğul olan şirkətlərdə iştirak təklifləri olan.\r\n\n" +
        "14.İş elanı yerləşdirilərkən, «Vəzifə» sütununda kateqoriyaya uyğun olan ikili mövqe qeyd oluna bilər. Məsələn, satıcı-kassir, barista-ofisiant, sürücü-ekspeditor, çörəkçi-şirniyyatçı və s.";

    #region Because of ComBoBox 
    private Category? _selectedCategory;
    private Education _selectedEducation;
    private WorkExperience _selectedWorkExperience;

    public ObservableCollection<Category> Categories { get; set; }
    public ObservableCollection<string> Elements { get; set; }
    public ObservableCollection<Education> EducationOptions { get; }
    public ObservableCollection<WorkExperience> WorkExperiences { get; }
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

    #endregion


    private JopAnnouncement? _jopAnnouncement;
    public JopAnnouncement JopAnnouncement
    {
        get { return _jopAnnouncement; }
        set
        {
            _jopAnnouncement = value;
            OnPropertyChanged(nameof(JopAnnouncement));
        }
    }

    public CategoryDbContext CategoryDbContext { get; set; }
    public JopAnnouncementDbContext JopDbContext { get; set; }
    public RelayCommand AddJopCommand { get; set; }
    public RelayCommand CvAddCommand { get; set; }

    private readonly INavigationService NavigationService;

    public AddJopAnnouncementModel(CategoryDbContext categoryDbContext,JopAnnouncementDbContext jopDbContext , INavigationService navigationService)
    {
        #region Because of Combobox
        Categories = categoryDbContext.Categories!;
        Elements = new ObservableCollection<string>();
        EducationOptions = new ObservableCollection<Education>((Enum.GetValues(typeof(Education)) as Education[])!);
        WorkExperiences = new ObservableCollection<WorkExperience>((Enum.GetValues(typeof(WorkExperience)) as WorkExperience[])!);
        #endregion

        NavigationService = navigationService;
        CategoryDbContext = categoryDbContext;
        JopDbContext = jopDbContext;
        AddJopCommand = new RelayCommand(AddJopClick);
        CvAddCommand = new RelayCommand(CvAddClick);
        JopAnnouncement = new();
    }

    private void CvAddClick(object? obj)
    {
        MainViewModel mainView = new(NavigationService);
        NavigationService.Navigate<AddCvAnnouncementView, AddCvAnnouncementViewModel>(mainView.CurrentPage2!);
    }

    private void AddJopClick(object? obj)
    {
        JopAnnouncement.SelectedCategory = new Dictionary<string, string>
        {
            {SelectedCategory.Name!,SelectedElement! }
        };
        JopAnnouncement.WorkExperience = SelectedWorkExperience;
        JopAnnouncement.Education = SelectedEducation;
            
        //---------- Add -------------------------
        JopDbContext.JopAnnouncements!.Add(JopAnnouncement);
        JopDbContext.SaveChanges();
        JopAnnouncement = new();
        //notifier.ShowSuccess("Jop Announcement Added Successfully");
        
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

}

using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Boos.az_WPF.ViewModels;

public class AddCvAnnouncementViewModel : ViewModel , INotifyPropertyChanged
{

    #region Because of ComBoBox 
    private Category? _selectedCategory;
    private Education _selectedEducation;
    private WorkExperience _selectedWorkExperience;
    private string? _selectedGender;
    private string imageSelected;
    private CvAnnouncement cvAnnouncement;

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
    public CategoryDbContext CategoryDbContext { get; set; }
    public CvAnnouncementDbContext CvDbContex {  get; set; }
    public string ImageSelected { get => imageSelected; set {imageSelected = value; OnPropertyChanged(); } }
    public CvAnnouncement CvAnnouncement { get => cvAnnouncement; set{ cvAnnouncement = value; OnPropertyChanged(); } }

    public AddCvAnnouncementViewModel(CategoryDbContext categoryDbContext,CvAnnouncementDbContext cvDbcontext)
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

        AddImageCommand = new RelayCommand(AddImageClick);
        AddCvCommand = new RelayCommand(AddCvClick);
        CvAnnouncement = new();
    }

    private void AddCvClick(object? obj)
    {
        CvAnnouncement.SelectedCategory = new Dictionary<string, string>
        {
            {SelectedCategory.Name!,SelectedElement! }
        };
        CvAnnouncement.WorkExperience = SelectedWorkExperience;
        CvAnnouncement.Education = SelectedEducation;
        CvAnnouncement.Image = ImageSelected;
        //---------- Add -------------------------
        CvDbContex.CvAnnouncements!.Add(CvAnnouncement);
        CvDbContex.SaveChanges();
        CvAnnouncement = new();
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
}

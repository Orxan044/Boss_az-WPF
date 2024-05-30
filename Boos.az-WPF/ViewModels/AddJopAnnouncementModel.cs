using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Boos.az_WPF.ViewModels;

public class AddJopAnnouncementModel : ViewModel , INotifyPropertyChanged
{

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

    
    public AddJopAnnouncementModel(CategoryDbContext categoryDbContext,JopAnnouncementDbContext jopDbContext)
    {
        #region Because of Combobox
        Categories = categoryDbContext.Categories!;
        Elements = new ObservableCollection<string>();
        EducationOptions = new ObservableCollection<Education>((Enum.GetValues(typeof(Education)) as Education[])!);
        WorkExperiences = new ObservableCollection<WorkExperience>((Enum.GetValues(typeof(WorkExperience)) as WorkExperience[])!);
        #endregion

        CategoryDbContext = categoryDbContext;
        JopDbContext = jopDbContext;
        AddJopCommand = new RelayCommand(AddJopClick);
        JopAnnouncement = new();
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

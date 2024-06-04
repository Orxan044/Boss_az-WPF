using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class SearchViewModel : ViewModel , INotifyPropertyChanged
{
    #region Because of Combobox
    private Category? _selectedCategory;
    private Education _selectedEducation;
    private WorkExperience _selectedWorkExperience;
    private string? _selectedCity;

    public ObservableCollection<Category> Categories { get; set; }
    public ObservableCollection<string> Elements { get; set; }
    public ObservableCollection<Education> EducationOptions { get; set; }
    public ObservableCollection<WorkExperience> WorkExperiences { get; set; } 
    public ObservableCollection<string> AzerbaijanCities { get; set; }
    public string? SelectedElement { get; set; }
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
    public string SelectedCity
    {
        get { return _selectedCity; }
        set
        {
            if (_selectedCity != value)
            {
                _selectedCity = value;
                OnPropertyChanged();
            }
        }
    }
    #endregion

    public CategoryDbContext CategoryDbContext { get; set; }
    public JopAnnouncementDbContext JopDbContext{ get; set; }
    public CvAnnouncementDbContext CvDbContext{ get; set; }

    public RelayCommand SearchCommand { get; set; }
    protected readonly INavigationService NavigationService;
    public SearchViewModel(CategoryDbContext categoryDbContext,JopAnnouncementDbContext jopDbContext,INavigationService navigationService, CvAnnouncementDbContext cvDbContext)
    {
        #region Because of Combobox
        Categories = categoryDbContext.Categories!;
        Elements = new ObservableCollection<string>();
        EducationOptions = new ObservableCollection<Education>((Education[])Enum.GetValues(typeof(Education)));
        WorkExperiences = new ObservableCollection<WorkExperience>((WorkExperience[])Enum.GetValues(typeof(WorkExperience)));
        AzerbaijanCities = new ObservableCollection<string> { "Vacib_deyil","Bakı", "Gəncə", "Sumqayıt", "Lənkəran", "Mingəçevir", "Masallı" };
        #endregion
        CategoryDbContext = categoryDbContext;
        JopDbContext = jopDbContext;
        CvDbContext = cvDbContext;
        NavigationService = navigationService;
        SearchCommand = new RelayCommand(SearchClick);
    }

    private void SearchClick(object? obj)
    {
        if (SelectedCity is not null || SelectedCategory is not null || SelectedElement is not null ||
            SelectedEducation is not Education.Vacib_deyil ||
            SelectedWorkExperience is not WorkExperience.Vacib_deyil)
        {
            
            if (MainViewModel.Check == false)
            {
                var query = JopDbContext.JopAnnouncements!.AsQueryable();


                if (SelectedCity is not null && SelectedCity != "Vacib_deyil")
                    query = query.Where(j => j.City == SelectedCity);

                if (SelectedElement is not null)
                    query = query.Where(j => j.SelectedCategory!.ContainsValue(SelectedElement));

                if (SelectedCategory is not null)
                    query = query.Where(j => j.SelectedCategory!.ContainsKey(SelectedCategory.Name!));


                if (SelectedEducation is not Education.Vacib_deyil)
                    query = query.Where(j => j.Education == SelectedEducation);

                if (SelectedWorkExperience is not WorkExperience.Vacib_deyil)
                    query = query.Where(j => j.WorkExperience == SelectedWorkExperience);

                MainViewModel mainView = new(NavigationService);
                var returnList = new ObservableCollection<JopAnnouncement>(query.ToList());
                if (returnList is not null)
                {
                    JopDbContext.JopAnnouncementsSearch!.Clear();
                    foreach (var item in returnList)
                        JopDbContext.JopAnnouncementsSearch!.Add(item);
                    NavigationService.Navigate<TypeAllView, TypeAllViewModel>(mainView.CurrentPage2!);
                }
            }

            if (MainViewModel.Check == true)
            {
                var query = CvDbContext.CvAnnouncements!.AsQueryable();


                if (SelectedCity is not null && SelectedCity != "Vacib_deyil")
                    query = query.Where(j => j.City == SelectedCity);

                if (SelectedElement is not null)
                    query = query.Where(j => j.SelectedCategory!.ContainsValue(SelectedElement));

                if (SelectedCategory is not null)
                    query = query.Where(j => j.SelectedCategory!.ContainsKey(SelectedCategory.Name!));


                if (SelectedEducation is not Education.Vacib_deyil)
                    query = query.Where(j => j.Education == SelectedEducation);

                if (SelectedWorkExperience is not WorkExperience.Vacib_deyil)
                    query = query.Where(j => j.WorkExperience == SelectedWorkExperience);

                MainViewModel mainView = new(NavigationService);
                var returnList = new ObservableCollection<CvAnnouncement>(query.ToList());
                if (returnList is not null)
                {
                    CvDbContext.CvAnnouncementsSearch!.Clear();
                    foreach (var item in returnList)
                        CvDbContext.CvAnnouncementsSearch!.Add(item);
                    NavigationService.Navigate<CvTypeAllView, TypeCvAllViewModel>(mainView.CurrentPage2!);
                }
            }
        }
        else
        {
            MainViewModel mainView = new(NavigationService);
            NavigationService.Navigate<TypeAllView, TypeAllViewModel>(mainView.CurrentPage2!);
        }
    }


    #region Because of Combobox
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

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

    public RelayCommand SearchCommand { get; set; }
    protected readonly INavigationService NavigationService;
    public SearchViewModel(CategoryDbContext categoryDbContext,JopAnnouncementDbContext jopDbContext,INavigationService navigationService)
    {
        #region Because of Combobox
        Categories = categoryDbContext.Categories!;
        Elements = new ObservableCollection<string>();
        EducationOptions = new ObservableCollection<Education>((Education[])Enum.GetValues(typeof(Education)));
        WorkExperiences = new ObservableCollection<WorkExperience>((WorkExperience[])Enum.GetValues(typeof(WorkExperience)));
        AzerbaijanCities = new ObservableCollection<string> { "Bakı", "Gəncə", "Sumqayıt", "Lənkəran", "Mingəçevir", "Masallı" };
        #endregion
        CategoryDbContext = categoryDbContext;
        JopDbContext = jopDbContext;
        NavigationService = navigationService;
        SearchCommand = new RelayCommand(SearchClick);
    }

    private void SearchClick(object? obj)
    {
        if (SelectedCity is not null || SelectedCategory is not null || SelectedEducation != Education.Vacib_deyil || SelectedWorkExperience is not WorkExperience.Vacib_deyil)
        {
            // Tüm kriterleri içeren bir IQueryable oluşturun
            var query = JopDbContext.JopAnnouncements!.AsQueryable();

            // Şehir kriteri seçildiyse, bu kriteri filtreye ekleyin
            if (SelectedCity is not null)
            {
                query = query.Where(j => j.City == SelectedCity);
            }

            // Diğer kriterler seçildiyse, bunları da filtreye ekleyin
            if (SelectedCategory!.Name is not null)
            {
                query = query.Where(j => j.SelectedCategory!.ContainsKey(SelectedCategory.Name));
            }

            if (SelectedEducation is not Education.Vacib_deyil)
            {
                query = query.Where(j => j.Education == SelectedEducation);
            }

            if (SelectedWorkExperience is not WorkExperience.Vacib_deyil)
            {
                query = query.Where(j => j.WorkExperience == SelectedWorkExperience);
            }

            // Sonuçları alın
            var sonuclar = new ObservableCollection<JopAnnouncement>(query.ToList());
            MainViewModel mainView = new(NavigationService);
            //TypeAllViewModel TypeAllViewModel = new(JopDbContext, NavigationService,sonuclar);
            //ypeAllViewModel.JopAnnouncements = sonuclar;
            NavigationService.Navigate<TypeAllView, TypeAllViewModel>(mainView.CurrentPage2!);
        }
        else
        {
            MainViewModel mainView = new(NavigationService);
            //TypeAllViewModel TypeAllViewModel = new(JopDbContext, NavigationService,);
            //TypeAllViewModel.JopAnnouncements = null!;
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

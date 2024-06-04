using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class CategoryViewModel : ViewModel
{
    public CategoryDbContext CategoryDbContext { get; set; }
    public JopAnnouncementDbContext JopDbContext { get; set; }

    public RelayCommand SearchCommand { get; set; }
    public RelayCommand CategoryCommand { get; set; }

    private readonly INavigationService NavigationService;
    public CategoryViewModel(CategoryDbContext categoryDbContext, INavigationService navigationService , JopAnnouncementDbContext jopDbContext)
    {
        CategoryDbContext = categoryDbContext;
        JopDbContext = jopDbContext;
        //Category category = new Category { Name = "İnformasiya texnologiyaları", Elements = CategoryDbContext.strings2, Count = 0 };
        //CategoryDbContext.Categories!.Add(category);
        //CategoryDbContext.SaveChanges();
        SearchCommand = new RelayCommand(SearchClick);
        CategoryCommand = new RelayCommand(CategoryClick);
        NavigationService = navigationService;
    }

    private void CategoryClick(object? obj)
    {
        string SelectedCategory = (obj as string)!;
        var query = JopDbContext.JopAnnouncements!.AsQueryable();
        if (SelectedCategory is not null)
            query = query.Where(j => j.SelectedCategory!.ContainsKey(SelectedCategory));

        MainViewModel mainView = new(NavigationService);
        var returnList = new ObservableCollection<JopAnnouncement>(query.ToList());
        if (returnList is not null)
        {
            JopDbContext.JopAnnouncementsSearch!.Clear();
            foreach (var item in returnList)
                JopDbContext.JopAnnouncementsSearch!.Add(item);

        }
        NavigationService.Navigate<TypeAllView, TypeAllViewModel>(mainView.CurrentPage2!);
    }

    private void SearchClick(object? obj)
    {
        MainViewModel mainView = new(NavigationService);
        NavigationService.Navigate<SearchView, SearchViewModel>(mainView.CurrentPage!);
    }
}

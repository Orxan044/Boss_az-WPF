using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class CategoryViewModel : ViewModel
{
    public CategoryDbContext CategoryDbContext { get; set; }
    public RelayCommand SearchCommand { get; set; }
    private readonly INavigationService NavigationService;
    public CategoryViewModel(CategoryDbContext categoryDbContext, INavigationService navigationService)
    {
        //CategoryDbContext = categoryDbContext;
        //Category category = new Category { Name = "İnformasiya texnologiyaları", Elements = CategoryDbContext.strings2, Count = 0 };
        //CategoryDbContext.Categories!.Add(category);
        //CategoryDbContext.SaveChanges();
        SearchCommand = new RelayCommand(SearchClick);
        NavigationService = navigationService;
    }

    private void SearchClick(object? obj)
    {
        MainViewModel mainView = new(NavigationService);
        NavigationService.Navigate<SearchView, SearchViewModel>(mainView.CurrentPage!);
    }
}

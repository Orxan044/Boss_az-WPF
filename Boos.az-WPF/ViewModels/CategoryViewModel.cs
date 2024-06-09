using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Reflection.Metadata;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class CategoryViewModel : ViewModel
{
    public CategoryDbContext CategoryDbContext { get; set; }
    public JopAnnouncementDbContext JopDbContext { get; set; }
    public CvAnnouncementDbContext CvDbContext { get; set; }

    public RelayCommand SearchCommand { get; set; }
    public RelayCommand CategoryCommand { get; set; }

    private readonly INavigationService NavigationService;
    public static string? SelectedCategory2 { get; set; }
    public CategoryViewModel(CategoryDbContext categoryDbContext, INavigationService navigationService , JopAnnouncementDbContext jopDbContext,CvAnnouncementDbContext cvDbContext)
    {
        CategoryDbContext = categoryDbContext;
        JopDbContext = jopDbContext;
        CvDbContext = cvDbContext;
        SearchCommand = new RelayCommand(SearchClick);
        CategoryCommand = new RelayCommand(CategoryClick);
        NavigationService = navigationService;
    }

    private void CategoryClick(object? obj)
    {
        
        MainViewModel mainViewModel = new(NavigationService);
        JopDbContext.JopAnnouncementsSearch!.Clear();
        foreach (var item in CategoryDbContext.Categories!)
        {
            if (item.Name == SelectedCategory2)
            {
                if (MainViewModel.Check == false)
                {
                    foreach (var item2 in JopDbContext.JopAnnouncements!)
                    {
                        if (item2.SelectedCategory!.ContainsKey(item.Name!))
                            JopDbContext.JopAnnouncementsSearch!.Add(item2);

                    }
                    NavigationService.Navigate<TypeAllView, TypeAllViewModel>(mainViewModel.CurrentPage2!);
                }
                if (MainViewModel.Check)
                {
                    foreach (var item2 in CvDbContext.CvAnnouncements!)
                    {
                        if (item2.SelectedCategory!.ContainsKey(item.Name!))
                            CvDbContext.CvAnnouncementsSearch!.Add(item2);

                    }
                    NavigationService.Navigate<CvTypeAllView, TypeCvAllViewModel>(mainViewModel.CurrentPage2!);
                }
            }
        }
    }

    private void SearchClick(object? obj)
    {
        MainViewModel mainView = new(NavigationService);
        NavigationService.Navigate<SearchView, SearchViewModel>(mainView.CurrentPage!);
    }
}

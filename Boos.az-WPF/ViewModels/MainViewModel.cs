using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Models;
using Boos.az_WPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UserPanel.Services.Navigation;

namespace Boos.az_WPF.ViewModels;

public class MainViewModel : ViewModel , INotifyPropertyChanged
{

    #region RelayCommand
    public RelayCommand BossAzCommand {  get; set; }
    public RelayCommand JopAnnouncementCommand { get; set; }
    public RelayCommand AddAnnouncementCommand { get; set; }
    #endregion


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

    public AllViewModel AllViewModel { get; set; }
    private readonly INavigationService navigationService;

    public MainViewModel(AllViewModel allView,INavigationService navigationService)
    {
        BossAzCommand = new RelayCommand(BossAzClick);
        JopAnnouncementCommand = new RelayCommand(JopAnnouncementClick);
        AddAnnouncementCommand = new RelayCommand(AddAnnouncementClick);

        //-------------------------------------------------
        currentPage = App.Container.GetInstance<CategoryView>();
        currentPage.DataContext = App.Container.GetInstance<CategoryViewModel>();
        //-------------------------------------------------

        //-------------------------------------------------
        currentPage2 = App.Container.GetInstance<AllView>();
        currentPage2.DataContext = App.Container.GetInstance<AllViewModel>();
        //-------------------------------------------------

        AllViewModel = allView;
        this.navigationService = navigationService;
    }

    public void AddAnnouncementClick(object? obj)
    {
        navigationService.Navigate<AddJopAnnouncement, AddJopAnnouncementModel>(CurrentPage2!);  
    }

    private void JopAnnouncementClick(object? obj)
    {
        AllViewModel.LatestAnnouncement = "SON İŞ ELANLARI";
        AllViewModel.LatestAnnouncementPeriumum = "PREMİUM İŞ ELANLARI";
    }

    private void BossAzClick(object? obj)
    {
        navigationService.Navigate<CategoryView,CategoryViewModel>(CurrentPage!);
        navigationService.Navigate<AllView,AllViewModel>(CurrentPage2!);
    }



    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------
}

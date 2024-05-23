using Boos.az_WPF.Command;
using Boos.az_WPF.Data;
using Boos.az_WPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Boos.az_WPF.ViewModels;

public class MainViewModel : ViewModel , INotifyPropertyChanged
{

    public RelayCommand BossAzCommand {  get; set; }

    private Page currentPage;

    public Page? CurrentPage
    {
        get => currentPage;
        set { currentPage = value!; OnPropertyChanged(); }
    }

    public MainViewModel()
    {
        BossAzCommand = new RelayCommand(BossAzClick);

        //-------------------------------------------------
        currentPage = App.Container.GetInstance<CategoryView>();
        currentPage.DataContext = App.Container.GetInstance<CategoryViewModel>();
        //-------------------------------------------------

    }

    private void BossAzClick(object? obj)
    {

    }



    //-------------------------------------------------------------
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? paramName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
    //-------------------------------------------------------------
}

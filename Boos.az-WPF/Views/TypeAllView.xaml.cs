using Boos.az_WPF.ViewModels;
using System.Windows.Controls;

namespace Boos.az_WPF.Views;


public partial class TypeAllView : Page
{
    public TypeAllView()
    {
        InitializeComponent();
    }

    private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (DataContext is TypeAllViewModel viewModel)
        {
            if (viewModel.ItemDoubleClickCommand!.CanExecute(null))
            {
                viewModel.ItemDoubleClickCommand.Execute(null);
            }
        }
    }
}

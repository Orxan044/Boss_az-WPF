using Boos.az_WPF.Data;

namespace Boos.az_WPF.ViewModels;

public class CategoryViewModel : ViewModel
{
    public CategoryDbContext CategoryDbContext { get; set; }
    public CategoryViewModel(CategoryDbContext categoryDbContext)
    {
        CategoryDbContext = categoryDbContext;
    }



}

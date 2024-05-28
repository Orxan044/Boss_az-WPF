using Boos.az_WPF.Data;
using Boos.az_WPF.Models;

namespace Boos.az_WPF.ViewModels;

public class CategoryViewModel : ViewModel
{
    public CategoryDbContext CategoryDbContext { get; set; }
    public CategoryViewModel(CategoryDbContext categoryDbContext)
    {
        CategoryDbContext = categoryDbContext;
        //Category category =  new Category  { Name = "Marketinq", Elements = CategoryDbContext.strings2, Count = 0 };
        //CategoryDbContext.Categories!.Add(category);
        //CategoryDbContext.SaveChanges();
    }



}

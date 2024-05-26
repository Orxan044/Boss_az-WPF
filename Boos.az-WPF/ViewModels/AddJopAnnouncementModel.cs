using Boos.az_WPF.Data;

namespace Boos.az_WPF.ViewModels;

public class AddJopAnnouncementModel : ViewModel
{
    public CategoryDbContext CategoryDbContext { get; set; }
    public AddJopAnnouncementModel(CategoryDbContext categoryDbContext)
    {
        CategoryDbContext = categoryDbContext;
    }
}

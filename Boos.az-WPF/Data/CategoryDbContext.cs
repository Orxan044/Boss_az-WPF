using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Boos.az_WPF.Data;

public class CategoryDbContext
{
    public ObservableCollection<Category>? Categories { get; set; }

    private string? fileName = "C:\\Users\\user\\source\\Repos\\Boss.az\\Boos.az-WPF\\JSON\\Category.json";

    public CategoryDbContext()
    {
        if (File.Exists(fileName))
        {
            var CategoriesJson = File.ReadAllText(fileName);
            Categories = JsonSerializer.Deserialize<ObservableCollection<Category>>(CategoriesJson) ?? new();
        }
        else
            Categories = new();
    }


    public void SaveChanges()
    {
        var CategoriesJson = JsonSerializer.Serialize(Categories);
        File.WriteAllText(fileName!, CategoriesJson);
    }
}

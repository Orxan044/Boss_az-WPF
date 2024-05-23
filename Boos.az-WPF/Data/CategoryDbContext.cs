using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Boos.az_WPF.Data;

public class CategoryDbContext
{
    //static Dictionary<string, int> d1 = new Dictionary<string, int>
    //    {
    //        { "Kredit Mütəxəssisi", 0 },
    //        { "Sigorta", 0 },
    //        { "Audit", 0 },
    //        { "Mühasibat", 0 },
    //        { "Maliyyə analiz", 0 },
    //        { "Bank xidməti", 0 },
    //        { "Kassir", 0 },
    //        { "İqtisadçı", 0 },
    //        { "Digər", 0 }
    //    };

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

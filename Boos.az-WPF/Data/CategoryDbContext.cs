using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Boos.az_WPF.Data;

public class CategoryDbContext
{
    //static public List<string> strings = new()
    //{
    //    "Kredit Mütəxəssisi" , "Sigorta" , "Audit" , "Mühasibat" ,"Maliyyə analiz","Kassir","Digər"
    //};

    static public List<string> strings2 = new()
    {
        "Sistem idarəetməsi" , "Məlumat bazasının idarə edilməsi və inkişafı" , " İT mütəxəssisi / məsləhətçi" , " Proqramlaşdırma" , "Digər"
    };

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

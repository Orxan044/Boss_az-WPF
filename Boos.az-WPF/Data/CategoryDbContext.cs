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

    //static public List<string> strings2 = new()
    //{
    //    "Marketinq menecment" , "İctimayətlə əlaqələr" , "Reklam" , "Kopiraytinq"
    //};

    public ObservableCollection<Category>? Categories { get; set; }

    private string? fileName = "C:\\Users\\Husey_so01\\Source\\Repos\\Boss_az-WPF\\Boos.az-WPF\\JSON\\Category.json";

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

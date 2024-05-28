using System.Collections.ObjectModel;

namespace Boos.az_WPF.Models;

public class Category
{
    public string? Name { get; set; }
    public List<string>? Elements { get; set; } 
    public int Count { get; set; } = 0;


}

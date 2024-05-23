using System.Collections.ObjectModel;

namespace Boos.az_WPF.Models;

public class Category
{
    public string? Name { get; set; }
    public Dictionary<string,int>? Elements { get; set; } 
    public int Count { get; set; } = 0;
   
}

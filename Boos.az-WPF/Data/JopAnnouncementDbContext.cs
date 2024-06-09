using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using Boos.az_WPF.Enum_Data;
using System.Text.Json;

namespace Boos.az_WPF.Data;

public class JopAnnouncementDbContext
{

    public ObservableCollection<JopAnnouncement>? JopAnnouncements { get; set; } = new();
    public ObservableCollection<JopAnnouncement>? JopAnnouncementCheck { get; set; } 
    public ObservableCollection<JopAnnouncement>? JopAnnouncementsPerimum { get; set; } = new();
    public ObservableCollection<JopAnnouncement>? JopAnnouncementsSearch { get; set; } = new();

    private string? fileName = "C:\\Users\\user\\source\\Repos\\Boss.az\\Boos.az-WPF\\JSON\\JopAnnouncement.json";

    public JopAnnouncementDbContext()
    {
        if (File.Exists(fileName))
        {
            var JopAnnouncementsJson = File.ReadAllText(fileName);
            JopAnnouncementCheck = JsonSerializer.Deserialize<ObservableCollection<JopAnnouncement>>(JopAnnouncementsJson) ?? new();
            foreach (var item in JopAnnouncementCheck)
            {
                if (item.AnnouncementType == AnnouncementType.New) JopAnnouncements!.Add(item);
                else JopAnnouncementsPerimum!.Add(item);
            }
        }
        else
            JopAnnouncements = new();
    }

    public JopAnnouncement? GetJopAnnouncement(string JopAnnouncementId)
    {
        JopAnnouncement New =  JopAnnouncements!.FirstOrDefault(p => p.Id.ToString() == JopAnnouncementId)!;
        JopAnnouncement Perium = JopAnnouncementsPerimum!.FirstOrDefault(p => p.Id.ToString() == JopAnnouncementId)!;
        if (New is not null) return New;
        else return Perium; 

    }

    public void Remove(JopAnnouncement JopAnnouncement) {
        var product = JopAnnouncements!.FirstOrDefault(x => x.Id == JopAnnouncement.Id);
        if (product is not null)    
            JopAnnouncements!.Remove(product);
    }
    

    public void SaveChanges()
    {
        foreach (var item in JopAnnouncements!)
        {
            if (item.AnnouncementType == AnnouncementType.PREMİUM)
            {
                JopAnnouncement Check = JopAnnouncementsPerimum!.FirstOrDefault(p => p == item)!;
                if (Check is null) JopAnnouncementsPerimum!.Add(item);
            }        
        }
        var JopAnnouncementJson = JsonSerializer.Serialize(JopAnnouncements);
        File.WriteAllText(fileName!, JopAnnouncementJson);
    }
}

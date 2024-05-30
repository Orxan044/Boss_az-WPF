using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using Boos.az_WPF.Enum_Data;
using System.Text.Json;

namespace Boos.az_WPF.Data;

public class JopAnnouncementDbContext
{

    public ObservableCollection<JopAnnouncement>? JopAnnouncements { get; set; }
    public ObservableCollection<JopAnnouncement>? JopAnnouncementsPerimum { get; set; }

    private string? fileName = "C:\\Users\\user\\source\\Repos\\Boss.az\\Boos.az-WPF\\JSON\\JopAnnouncement.json";

    public JopAnnouncementDbContext()
    {
        if (File.Exists(fileName))
        {
            var JopAnnouncementsJson = File.ReadAllText(fileName);
            JopAnnouncements = JsonSerializer.Deserialize<ObservableCollection<JopAnnouncement>>(JopAnnouncementsJson) ?? new();
        }
        else
            JopAnnouncements = new();
    }

    public JopAnnouncement? GetJopAnnouncement(string JopAnnouncementId) =>
        JopAnnouncements!.FirstOrDefault(p => p.Id.ToString() == JopAnnouncementId);
    



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

using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Boos.az_WPF.Data;

public class CvAnnouncementDbContext
{
    public ObservableCollection<CvAnnouncement>? CvAnnouncements { get; set; }
    public ObservableCollection<CvAnnouncement>? CvAnnouncementsPerimum { get; set; }
    public ObservableCollection<CvAnnouncement>? CvAnnouncementsSearch { get; set; } = new();

    private string? fileName = "C:\\Users\\user\\source\\Repos\\Boss.az\\Boos.az-WPF\\JSON\\CvAnnouncement.json";

    public CvAnnouncementDbContext()
    {
        if (File.Exists(fileName))
        {
            var CvAnnouncementsJson = File.ReadAllText(fileName);
            CvAnnouncements = JsonSerializer.Deserialize<ObservableCollection<CvAnnouncement>>(CvAnnouncementsJson) ?? new();
        }
        else
            CvAnnouncements = new();
    }

    public CvAnnouncement? GetJopAnnouncement(string CvAnnouncementId) =>
        CvAnnouncements!.FirstOrDefault(p => p.Id.ToString() == CvAnnouncementId);


    public void SaveChanges()
    {
        foreach (var item in CvAnnouncements!)
        {
            if (item.AnnouncementType == AnnouncementType.PREMİUM)
            {
                CvAnnouncement Check = CvAnnouncementsPerimum!.FirstOrDefault(p => p == item)!;
                if (Check is null) CvAnnouncementsPerimum!.Add(item);
            }
        }
        var CvAnnouncementJson = JsonSerializer.Serialize(CvAnnouncements);
        File.WriteAllText(fileName!, CvAnnouncementJson);
    }

}

using Boos.az_WPF.Enum_Data;
using Boos.az_WPF.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Boos.az_WPF.Data;

public class CvAnnouncementDbContext
{
    public ObservableCollection<CvAnnouncement>? CvAnnouncements { get; set; } = new();
    public ObservableCollection<CvAnnouncement>? CvAnnouncementsCheck { get; set; }
    public ObservableCollection<CvAnnouncement>? CvAnnouncementsPerimum { get; set; } = new();
    public ObservableCollection<CvAnnouncement>? CvAnnouncementsSearch { get; set; } = new();

    private string? fileName = "C:\\Users\\user\\source\\Repos\\Boss.az\\Boos.az-WPF\\JSON\\CvAnnouncement.json";

    public CvAnnouncementDbContext()
    {
        if (File.Exists(fileName))
        {
            var CvAnnouncementsJson = File.ReadAllText(fileName);
            CvAnnouncementsCheck = JsonSerializer.Deserialize<ObservableCollection<CvAnnouncement>>(CvAnnouncementsJson) ?? new();
            foreach (var item in CvAnnouncementsCheck)
            {
                if (item.AnnouncementType == AnnouncementType.New) CvAnnouncements.Add(item);
                else CvAnnouncementsPerimum.Add(item);
            }
        }
        else
            CvAnnouncements = new();
    }

    public CvAnnouncement? GetCvAnnouncement(string CvAnnouncementId)
    {
        CvAnnouncement New = CvAnnouncements!.FirstOrDefault(p => p.Id.ToString() == CvAnnouncementId)!;
        CvAnnouncement Perium = CvAnnouncementsPerimum!.FirstOrDefault(p => p.Id.ToString() == CvAnnouncementId)!;
        if (New is not null) return New;
        else return Perium;
    }


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

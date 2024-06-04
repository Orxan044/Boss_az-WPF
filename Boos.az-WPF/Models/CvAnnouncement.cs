using Boos.az_WPF.Enum_Data;

namespace Boos.az_WPF.Models;

public class CvAnnouncement : Entity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? FatherName { get; set; }
    public List<string>? Gender { get; set; }
    public int? Age { get; set; }
    public string? Image {get; set; }
    public Education? Education {get; set; }
    public string? More {get; set; }
    public WorkExperience? WorkExperience {get; set; }
    public Dictionary<string, string>? SelectedCategory { get; set; }
    public string? Position { get; set; }
    public string? City { get; set; }
    public string? SalaryMin { get; set; }
    public string? Skills { get; set; }
    public string? Add_Information { get; set; }
    public string? Mail { get; set; }
    public string? PhoneNumber { get; set; }
    public AnnouncementType AnnouncementType { get; set; }
    public string? FullName { get => $"{Name} {Surname}";}
    public string? SalaryRange { get => $"{SalaryMin} AZN"; }
    public string? StartAnnouncementTime { get => $"{DateTime.Now.ToString("MMMM")} {DateTime.Now.Day} , {DateTime.Now.Year}"; set { } }

    private DateTime DateTimeData = DateTime.Now.AddMonths(1);
    public string? EndAnnouncementTime { get => $"{DateTimeData.ToString("MMMM")} {DateTimeData.Day} , {DateTimeData.Year}"; set { } }
}

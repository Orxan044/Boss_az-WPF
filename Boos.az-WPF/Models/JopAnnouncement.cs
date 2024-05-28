using Boos.az_WPF.Enum_Data;

namespace Boos.az_WPF.Models;

public class JopAnnouncement : Entity
{
    public string? Mail {  get; set; }
    public string? PhoneNumber { get; set; }
    public Dictionary<string,string>? SelectedCategory { get; set; }
    public string? Position { get; set; }
    public string? City { get; set; }
    public int? SalaryMin { get; set; }
    public int? SalaryMax { get; set; }
    public string? SalaryRange
    {
        get
        {
            return $"{SalaryMin} - {SalaryMax} AZN";
        }
        set { }
    }
    public int? AgeMin { get; set; }
    public int? AgeMax { get; set; }
    public Education? Education { get; set; }
    public WorkExperience? WorkExperience { get; set; }
    public string? CandidateRequirements { get; set; }
    public string? JobInformation { get; set; }
    public string? CompanyName { get; set; }
    public string? RelevantPersonName { get; set; }
    public DateTime? StartAnnouncementTime { get; set; } = DateTime.Now;
    public DateTime? EndAnnouncementTime { get; set; } = DateTime.Now.AddMonths(1);
    public AnnouncementType? AnnouncementType { get; set; } = Enum_Data.AnnouncementType.New;

}

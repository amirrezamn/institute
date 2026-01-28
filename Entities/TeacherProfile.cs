using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class TeacherProfile
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    [MaxLength(50)]
    public string Speciality { get; set; } = string.Empty;
    [MaxLength(50)]
    public int ExperienceYears { get; set; } 
    public bool IsDeleted { get; set; } = false;
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;

    // Navigation
    public ICollection<ClassRoom> Classes { get; set; } = [];
}
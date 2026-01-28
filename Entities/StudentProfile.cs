using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class StudentProfile:User
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    [MaxLength(50)]
    public string UserName { get; set; }=string.Empty;
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    public string Level { get; set; } = "Beginner";
    public bool IsDeleted { get; set; } = false;
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = null!;
    // Navigation
    public ICollection<Enrollment> Enrollments { get; set; } = [];
    public ICollection<Attendance> Attendances { get; set; } = [];
}
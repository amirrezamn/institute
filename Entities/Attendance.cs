using institute.Enums;

namespace institute.Entities;

public class Attendance
{
    public int Id { get; set; }

    public int SessionId { get; set; }
    
    public ClassSession Session { get; set; } = null!;

    public int StudentId { get; set; }
    public StudentProfile Student { get; set; } = null!;
   
    public AttendanceStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsDeleted { get; set; }
}
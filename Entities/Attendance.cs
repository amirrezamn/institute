namespace institute.Entities;

public class Attendance
{
    public int Id { get; set; }

    public int SessionId { get; set; }
    public ClassSession Session { get; set; } = null!;

    public int StudentId { get; set; }
    public StudentProfile Student { get; set; } = null!;

    public bool IsPresent { get; set; } = false;
}
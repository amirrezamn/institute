namespace institute.Entities;

public class Enrollment
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public StudentProfile Student { get; set; } = null!;

    public int ClassRoomId { get; set; }
    public ClassRoom ClassRoom { get; set; } = null!;

    public DateTime EnrollDate { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}
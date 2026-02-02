namespace institute.Entities;

public class ClassSession
{
    public int Id { get; set; }
    public int ClassRoomId { get; set; }
    public ClassRoom ClassRoom { get; set; } = null!;
    public DateOnly SessionDate { get; set; }
    public bool IsDeleted { get; set; }


    // Navigation
    public ICollection<Attendance> Attendances { get; set; } = [];
}
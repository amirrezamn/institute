using institute.Interfaces;

namespace institute.Entities;

public class Grade:ISoftDelete
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public StudentProfile Student { get; set; } = null!;
    public int ClassRoomId { get; set; }
    public ClassRoom ClassRoom { get; set; } = null!;
    public decimal TotalScore { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    
    public decimal ClassActivity { get; set; }
    public decimal Speaking { get; set; }
    public decimal Listening { get; set; }
    public decimal Writing { get; set; }
    public decimal Midterm { get; set; }
    public decimal FinalExam { get; set; }
    public bool IsPassed { get; set; }

}
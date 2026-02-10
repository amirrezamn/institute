using institute.Entities;

namespace institute.DTOs;

public class GiveGradeDto
{
    
    public int StudentId { get; set; }
    public int ClassRoomId { get; set; }
    public decimal ClassActivity { get; set; }
    public decimal Speaking { get; set; }
    public decimal Listening { get; set; }
    public decimal Writing { get; set; }
    public decimal Midterm { get; set; }
    public decimal FinalExam { get; set; }}
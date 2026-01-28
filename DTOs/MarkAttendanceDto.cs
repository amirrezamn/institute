namespace institute.DTOs;

public class MarkAttendanceDto
{
    public int ClassId { get; set; }
    public int StudentUserId { get; set; }
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
}
using institute.Enums;

namespace institute.DTOs;

public class AttendancePageDto
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = null!;
    public AttendanceStatus Status { get; set; }
}
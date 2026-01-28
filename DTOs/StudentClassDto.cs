using institute.Entities;

namespace institute.DTOs;

public class StudentClassDto
{
    public int ClassRoomId { get; set; }
    public string CourseTitle { get; set; } = null!;
    public string TermTitle { get; set; } = null!;
    public string TeacherName { get; set; } = null!;
}
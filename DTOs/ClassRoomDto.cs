using institute.Entities;

namespace institute.DTOs;

public class ClassRoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }

    public int TermId { get; set; }
    public Term TermName { get; set; } = null!;

    public int CourseId { get; set; }
    public Course CourseTitle { get; set; } = null!;

    public int TeacherId { get; set; }
    public TeacherProfile TeacherName { get; set; } = null!;
}
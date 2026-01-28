namespace institute.DTOs;

public class UpdateCourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = null!;
    public int Level { get; set; }
}
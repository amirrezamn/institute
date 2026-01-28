namespace institute.DTOs;

public class ClassRoomPageDto
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }

    public int TermId { get; set; }

    public int CourseId { get; set; }

    public int TeacherId { get; set; }
}
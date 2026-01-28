namespace institute.DTOs;

public class TeacherWithClassesDto
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Speciality { get; set; } = null!;
    public int ExperienceYears { get; set; }

    public List<ClassRoomDto> Classes { get; set; } = new();
}
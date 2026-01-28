namespace institute.DTOs;

public class TeacherPageDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Speciality { get; set; } = null!;
    public int ExperienceYears { get; set; }

    public int ClassesCount { get; set; }
}
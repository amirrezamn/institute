namespace institute.DTOs;

public class CreateTeacherDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Speciality { get; set; } = null!;
    public int ExperienceYears { get; set; }
}
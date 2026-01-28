namespace institute.DTOs;

public class RegisterRequestDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    // اختیاری: نقش پیش‌فرض
    public string Role { get; set; } = "Student";
}
namespace institute.DTOs;

public class UpdateAdminDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Department { get; set; } = "Management";
}
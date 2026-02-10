namespace institute.DTOs;

public class PasswordResetDto
{
    
    public string Token { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
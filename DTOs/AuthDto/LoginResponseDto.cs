namespace institute.DTOs;

public class LoginResponseDto
{
    public string AccessToken { get; set; } = null!;
    public DateTime ExpireAt { get; set; }
    public List<string> Roles { get; set; } = new();
}
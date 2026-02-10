using institute.Interfaces;

namespace institute.Entities;

public class PasswordResetToken:ISoftDelete
{
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpireAt { get; set; }
    public bool IsUsed { get; set; } = false;
    
    public bool IsDeleted { get; set; } = false;

    public User User { get; set; } = null!;
}
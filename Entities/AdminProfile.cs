using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class AdminProfile
{
    [Key]
    public int UserId { get; set; }
    [MaxLength(50)]
    public string FullName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Department { get; set; } = "Management";
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;
    

    // Navigation
    public User User { get; set; } = null!;
}
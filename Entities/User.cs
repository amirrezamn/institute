using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(50)]
    [Required]
    public string FullName { get; set; } = null!;
    [MaxLength(50)]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [MaxLength(250)]
    [Required]
    public string PasswordHash { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public StudentProfile? StudentProfile { get; set; }
    public TeacherProfile? TeacherProfile { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
    
    public AdminProfile? AdminProfile { get; set; }

}

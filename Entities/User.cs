using System.ComponentModel.DataAnnotations;
using institute.Interfaces;

namespace institute.Entities;

public class User : ISoftDelete
{
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string FullName { get; set; } = null!;

    [MaxLength(100)]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    // BCrypt حدود 60 کاراکتر تولید می‌کند → 250 عالیه
    [MaxLength(250)]
    [Required]
    public string PasswordHash { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    // برای Global Query Filter
    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // -------------------- Navigation --------------------

    public StudentProfile? StudentProfile { get; set; }
    public TeacherProfile? TeacherProfile { get; set; }
    public AdminProfile? AdminProfile { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
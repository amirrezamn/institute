using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class Role
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
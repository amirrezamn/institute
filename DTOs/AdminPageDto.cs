using System.ComponentModel.DataAnnotations;

namespace institute.DTOs;

public class AdminPageDto
{
    [MaxLength(50)]
    public string FullName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Department { get; set; } = "Management";
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;
}
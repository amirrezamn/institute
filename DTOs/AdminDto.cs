namespace institute.DTOs;

public class AdminDto
{
    public string FullName { get; set; } = null!; 
    public string Email { get; set; } = null!;

    public string Department { get; set; } = "Management";
    public string PhoneNumber { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
}
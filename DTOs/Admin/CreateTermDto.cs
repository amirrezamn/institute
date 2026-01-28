namespace institute.DTOs.Admin;

public class CreateTermDto
{
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
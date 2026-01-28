namespace institute.DTOs;

public class UpdateTermDto
{
    public int TermId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
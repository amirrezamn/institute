using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class Term
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public ICollection<ClassRoom> ClassRooms { get; set; } = [];
}
using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class Course
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool IsDeleted { get; set; }
    
}    
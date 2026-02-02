using System.ComponentModel.DataAnnotations;

namespace institute.Entities;

public class ClassRoom
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }

    public int TermId { get; set; }
    public Term Term { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int TeacherId { get; set; }
    public bool IsDeleted { get; set; }

    public TeacherProfile Teacher { get; set; } = null!;

    // Navigation
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<ClassSession> Sessions { get; set; } = new List<ClassSession>();
    
}
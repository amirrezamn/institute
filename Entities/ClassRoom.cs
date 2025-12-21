namespace institute.Entities;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int TermId { get; set; }
    public Term Term { get; set; } = null!;

    public int TeacherId { get; set; }
    public TeacherProfile TeacherProfile { get; set; } = null!;

    public ICollection<StudentClass> StudentClasses { get; set; } = [];
}
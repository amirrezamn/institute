namespace institute.Entities;

public class Student
{
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    public ICollection<StudentClass> StudentClasses { get; set; } = [];
}
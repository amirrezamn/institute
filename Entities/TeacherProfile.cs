namespace institute.Entities;

public class Teacher
{
    public string Speciality { get; set; } = null!;
    public ICollection<Class> Classes { get; set; } = [];
}
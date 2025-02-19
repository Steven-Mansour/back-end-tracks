namespace DemoLab5.Domain.Entities;

public class Teacher
{
    public int TeacherId { get; set; }
    public string FullName { get; set; }
    
    public List<Course> Courses { get; set; } = new List<Course>();
}

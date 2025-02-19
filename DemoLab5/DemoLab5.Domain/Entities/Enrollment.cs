namespace DemoLab5.Domain.Entities;

public class Enrollment
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
        
    public int CourseId { get; set; }
    public Course Course { get; set; }
        
    public decimal Grade { get; set; }
}
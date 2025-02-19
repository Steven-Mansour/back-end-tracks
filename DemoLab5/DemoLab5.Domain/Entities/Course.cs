namespace DemoLab5.Domain.Entities;

public class Course
{
   
    public int CourseId { get; set; }
    public string Title { get; set; }
        
    public DateTime TimeSlot { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
        
    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}
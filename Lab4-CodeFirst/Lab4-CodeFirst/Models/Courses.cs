using System.Text.Json.Serialization;

namespace Lab4_CodeFirst.Models;

public class Courses
{
    public int CourseId { get; set; } 
    public required string CourseName { get; set; }
   
    public virtual ICollection<Classes> Classes { get; set; } = new List<Classes>();
}
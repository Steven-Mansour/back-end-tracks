using System.Text.Json.Serialization;

namespace Lab4_CodeFirst.Models;

public class Classes
{
    public int ClassId { get; set; }

    public int? CourseId { get; set; }
    public virtual Courses? Course { get; set; }  

    public  int? TeacherId { get; set; }  
    public virtual Teachers? Teacher { get; set; } 
    
   
    public virtual ICollection<Students> Students { get; set; } = new List<Students>();
}
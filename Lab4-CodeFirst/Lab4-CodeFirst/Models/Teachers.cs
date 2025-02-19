using System.Text.Json.Serialization;

namespace Lab4_CodeFirst.Models;

public class Teachers
{
    public int TeacherId { get; set; } 
    public required string FirstName { get; set; } 
    public required string LastName { get; set; }
    public required string Email { get; set; } 
    
    
    public virtual ICollection<Classes> Classes { get; set; } = new List<Classes>();
}
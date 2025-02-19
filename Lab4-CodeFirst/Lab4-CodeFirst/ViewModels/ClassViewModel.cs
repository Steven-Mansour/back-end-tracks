namespace Lab4_CodeFirst.ViewModels;

public class ClassViewModel
{
 
    public string CourseName { get; set; }
    public string TeacherName { get; set; }
    public int ClassId { get; set; }
    public int? CourseId { get; set; }
    public int? TeacherId { get; set; }
    public CourseViewModel? Course { get; set; }
    public TeacherViewModel? Teacher { get; set; }
    public List<StudentNameViewModel> Students { get; set; } = new();
}
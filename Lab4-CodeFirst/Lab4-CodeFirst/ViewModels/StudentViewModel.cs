namespace Lab4_CodeFirst.ViewModels;

public class StudentViewModel
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<ClassNameViewModel> Classes { get; set; } = new();
}
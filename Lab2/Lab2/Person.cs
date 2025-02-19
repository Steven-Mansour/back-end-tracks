using Lab2.Services;

namespace Lab2;

public class Person
{
    public long Id { get; set; }
    
    [MapFrom("Name")]
    public  string FullName { get; set; }
    
    [MapFrom("Email")]
    public string ContactEmail { get; set; }  
}
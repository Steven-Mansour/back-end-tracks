namespace lab3.Models;

record Author
{
    public int AuthorId { get; set; }
    public required string  Name { get; set; }
    public DateTime BirthDate { get; set; }
    public required string Country { get; set; }
    
    
}
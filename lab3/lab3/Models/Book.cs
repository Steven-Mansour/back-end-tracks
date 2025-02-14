namespace lab3.Models;

record Book
{
    public int BookId { get; set; }
    public required string Title { get; set; }
    public required int AuthorId { get; set; }
    public required string Isbn { get; set; }
    public required int PublishedYear { get; set; }
    
    public required DateTime ReleaseDate { get; set; }
}

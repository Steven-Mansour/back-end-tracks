using lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab3.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController: ControllerBase
{
     List<Author> authors = new()
    {
        new Author { AuthorId = 1, Name = "Author1", BirthDate = new DateTime(1975, 4, 20), Country = "USA" },
        new Author { AuthorId = 2, Name = "Author2", BirthDate = new DateTime(1980, 3, 15), Country = "UK" },
        new Author { AuthorId = 3, Name = "Author3", BirthDate = new DateTime(1990, 10, 10), Country = "Canada" },
        new Author { AuthorId = 4, Name = "Author4", BirthDate = new DateTime(1975, 5, 20), Country = "USA" }, 
        new Author { AuthorId = 5, Name = "Author5", BirthDate = new DateTime(1980, 4, 15), Country = "Canada" }   
    };

    List<Book> books = new()
    {
        new Book { BookId = 1, Title = "Book1", AuthorId = 1, Isbn = "ISBN1", PublishedYear = 2001, ReleaseDate = new DateTime(2001, 6, 15) },
        new Book { BookId = 2, Title = "Book2", AuthorId = 2, Isbn = "ISBN2", PublishedYear = 2005, ReleaseDate = new DateTime(2005, 3, 10) },
        new Book { BookId = 3, Title = "Book3", AuthorId = 3, Isbn = "ISBN3", PublishedYear = 2010, ReleaseDate = new DateTime(2010, 9, 5) },
        new Book { BookId = 4, Title = "Book4", AuthorId = 1, Isbn = "ISBN4", PublishedYear = 2001, ReleaseDate = new DateTime(2001, 1, 20) },  
        new Book { BookId = 5, Title = "Book5", AuthorId = 2, Isbn = "ISBN5", PublishedYear = 2005, ReleaseDate = new DateTime(2005, 3, 10) },  
        new Book { BookId = 6, Title = "Book6", AuthorId = 4, Isbn = "ISBN6", PublishedYear = 2015, ReleaseDate = new DateTime(2015, 7, 22) },
        new Book { BookId = 7, Title = "Book7", AuthorId = 5, Isbn = "ISBN7", PublishedYear = 2020, ReleaseDate = new DateTime(2020, 2, 18) },
        new Book { BookId = 8, Title = "Book8", AuthorId = 3, Isbn = "ISBN8", PublishedYear = 2010, ReleaseDate = new DateTime(2010, 9, 5) },  
        new Book { BookId = 9, Title = "Book9", AuthorId = 1, Isbn = "ISBN9", PublishedYear = 2001, ReleaseDate = new DateTime(2001, 6, 15) },  
        new Book { BookId = 10, Title = "Book10", AuthorId = 5, Isbn = "ISBN10", PublishedYear = 2022, ReleaseDate = new DateTime(2022, 5, 30) }
    };
    
    [HttpGet("books/year/{year}")]
    public IActionResult GetBooksByYear(int year, [FromQuery] string order = "asc")
    {
        var filteredBooks = books.Where(b => b.PublishedYear == year);

        if (order.ToLower() == "desc")
            filteredBooks = filteredBooks.OrderByDescending(b => b.ReleaseDate);
        else
            filteredBooks = filteredBooks.OrderBy(b => b.ReleaseDate);

        return filteredBooks.Any()? Ok(filteredBooks) : NotFound($"No books from year {year} are found");
    }

    [HttpGet("authors/same_birth_year")]
    public IActionResult GetAuthorsBySameBirthYear()
    {
        var filteredAuthors = authors
            .GroupBy(a => a.BirthDate.Year )
            .Where(a => a.Count() > 1);
        return filteredAuthors.Any()? Ok(filteredAuthors): NotFound($"No authors are born on the same year");
    }
    
    [HttpGet("authors/same_birth_year_and_country")]
    public IActionResult GetAuthorsBySameBirthYearAndCountry()
    {
        var filteredAuthors = authors
            .GroupBy(a => (a.BirthDate.Year, a.Country) )
            .Where(a => a.Count() > 1);
        return filteredAuthors.Any()? Ok(filteredAuthors): NotFound($"No authors are born on the same year and in the same country");
    }
    
    [HttpGet("books/count")]
    public IActionResult CountBooks()
    {
        var booksCount = books.Count();
        return Ok($"There are {booksCount} books in the library.");
    }

    [HttpGet("books/pages")]
    public IActionResult GetBookPages(int page = 1)
    {
        int size = 3;
        if (page < 1) page = 1;
        var filteredBooks = books
            .Skip((page - 1) * size)
            .Take(size);

        return Ok(filteredBooks);
    }
    
    

    
}
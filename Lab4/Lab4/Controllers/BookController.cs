using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Controllers;

public class BookController: ODataController
{
    private readonly LibrarydbContext _context;

    public BookController(LibrarydbContext context)
    {
        _context = context;
    }
    [EnableQuery]
    [HttpGet("odata/Book/BooksByYear")]
    public IActionResult GetBookByYear(int year, string order = "asc")
    {
        var books = _context.Books.Where(b => b.PublishedYear == year);

        if (order.ToLower() == "asc")
            books = books.OrderBy(b => b.PublishedYear);
        else 
            books = books.OrderByDescending(b => b.PublishedYear);
        
        return Ok(books);
    }
    
    [EnableQuery]
    [HttpGet("odata/Book/BookPage")]
    public IActionResult GetBookPage(int page = 1)
    {
        int size = 3;
        var books = _context.Books
            .Skip((page-1)*size)
            .Take(size);
        return Ok(books);
    }
    
    //By default, there is a book count route that returns
    //the number of books in the database
    [EnableQuery]
    public IActionResult Get()
    {
        var book = _context.Books;
    
        return Ok(book);
    }
    
   
    
}
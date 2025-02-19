using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Controllers;
public class AuthorController: ODataController
{
    private readonly LibrarydbContext _context;

    public AuthorController(LibrarydbContext context)
    {
        _context = context;
    }
    
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Authors);
    }
    
    [EnableQuery]
    [HttpGet("odata/Author/AuthorsByYear")]
    public IActionResult GetAuthorsByYear()
    {
        var groupedAuthors = _context.Authors
            .GroupBy(a => a.BirthDate.Value.Year)
            .Select(g => new
            {
                BirthYear = g.Key,
                Authors = g.ToList()
            });

        return Ok(groupedAuthors);
    }
    
    [EnableQuery]
    [HttpGet("odata/Author/AuthorsByYearAndCountry")]
    public IActionResult GetAuthorsByYearAndCountry()
    {
        var groupedAuthors = _context.Authors
            .GroupBy(a => new { BirthYear = a.BirthDate.Value.Year, a.Country }) 
            .Select(g => new
            {
                BirthYear = g.Key.BirthYear,
                Country = g.Key.Country,
                Authors = g.ToList()
            });

        return Ok(groupedAuthors);
    }
    
    
    
    
    
}
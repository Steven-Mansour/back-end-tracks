using System.Globalization;
using lab2.Filters;
using Lab2.Services;
using Microsoft.AspNetCore.Mvc;
using lab2.Services;

namespace Lab2.Controllers;

[ApiController]
[Route("api/users")]
//[ServiceFilter(typeof(LoggingActionFilter))]
public class UserController : ControllerBase
{
    private readonly IDateService _dateService;
    private readonly ObjectMapperService _mapperService;

    public UserController(IDateService dateService, ObjectMapperService mapperService)
    {
        _dateService = dateService;
        _mapperService = mapperService;
    }
    
    private static List<User> users = new List<User> {
        new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
        new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" },
        new User { Id = 3, Name = "Alice", Email = "alice@example.com" }
    };

    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        return users;
    }
    
    [HttpPost("map-to-person")]
    public IActionResult MapUserToPerson([FromBody] User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        Person person = _mapperService.Map<User, Person>(user);

        return Ok(person);
    }
    
    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id must be greater than 0");
        }
        var user = users.Find(u => u.Id == id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPost("update")]
    public ActionResult<User> UpdateUser( [FromBody] User user)
    {
        var user1 = users.Find(u => u.Id == user.Id);
        if (user1 == null || user1.Email == null || user1.Name == null) 
                throw new ArgumentException("User properties cannot be null");
        user1.Name = user.Name;
        user1.Email = user.Email;
        return user;
    }
    
   
    [HttpGet("search")]
    public ActionResult<IEnumerable<User>> SearchUsers([FromQuery] string name)
    {
        if (name == null || name.Length==0)
        {
            throw new ArgumentException("Name cannot be null");
        }
        var filteredUsers = users.Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredUsers;
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id must be greater than 0");
        }
        var user = users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        users.Remove(user);
        return NoContent();
    }
    [HttpGet("current-date")]
    public IActionResult GetCurrentDate()
    {
        var acceptedLanguage = Request.Headers["Accept-Language"].ToString();
        try
        {
            var date = _dateService.GetFormattedDate(acceptedLanguage);
            return Ok(date);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred");
        }

        return Ok();
    }

    [HttpPost("PostFile")]
    public ActionResult PostFile(IFormFile uploadedFile)
    {            
        var saveFilePath = Path.Combine("wwwroot", uploadedFile.FileName);
        using (var stream = new FileStream(saveFilePath, FileMode.Create))
        {
            uploadedFile.CopyToAsync(stream);
        }
        return Ok();
    }


}
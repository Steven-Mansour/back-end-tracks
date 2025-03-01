
using DemoLab7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly UniversityDbContext _context;

    public CourseController(UniversityDbContext context)
    {
        _context = context;
    }
    [Authorize(Roles = "teacher")]
    [HttpPost("add")]
    public async Task<IActionResult> AddCourse([FromBody] Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return Ok("Course created successfully");
    }
    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _context.Courses
            .Include(c => c.Classes) 
            .ToListAsync();

        return Ok(courses);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Classes) 
            .FirstOrDefaultAsync(c => c.CourseId == id);

        if (course == null)
        {
            return NotFound();
        }
        
        return Ok(course);
    }
    
}
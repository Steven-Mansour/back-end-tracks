using AutoMapper;
using Lab4_CodeFirst.Models;
using Lab4_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CourseController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCourse([FromBody] Courses course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return Ok("Course created successfully");
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _context.Courses
            .Include(c => c.Classes) 
            .ToListAsync();

        var courseViewModels = _mapper.Map<List<CourseViewModel>>(courses);
        return Ok(courseViewModels);
    }

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

        var courseViewModel = _mapper.Map<CourseViewModel>(course);
        return Ok(courseViewModel);
    }
    
}
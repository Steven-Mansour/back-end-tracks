using DemoLab7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeacherController: ControllerBase
{
    private readonly UniversityDbContext _context;

    public TeacherController(UniversityDbContext context)
    {
        _context = context;
    }
    [Authorize(Roles = "teacher")]
    [HttpPost("add")]
    public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return Ok("Created teacher successfully");
    }
    [Authorize(Roles = "teacher")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllTeachers()
    {
        var teachers = await _context.Teachers
            .Include(t => t.Classes) 
            .ToListAsync();
        return Ok(teachers);
    }
    [Authorize(Roles = "teacher")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeacherById(int id)
    {
        var teacher = await _context.Teachers
            .Include(t => t.Classes) 
            .FirstOrDefaultAsync(t => t.TeacherId == id);
        if (teacher == null) return NotFound();
        return Ok(teacher);
    }
}
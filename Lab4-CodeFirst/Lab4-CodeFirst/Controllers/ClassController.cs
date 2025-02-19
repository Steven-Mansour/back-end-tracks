using AutoMapper;
using Lab4_CodeFirst.Models;
using Lab4_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClassController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public ClassController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddClass([FromBody] Classes newClass)
    {
        var course = await _context.Courses.FindAsync(newClass.CourseId);
        var teacher = await _context.Teachers.FindAsync(newClass.TeacherId);

        if (course == null || teacher == null)
        {
            return BadRequest("Invalid course or teacher ID.");
        }


        _context.Classes.Add(newClass);
        await _context.SaveChangesAsync();

        return Ok("Class added successfully");
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllClasses()
    {
        var classes = await _context.Classes
            .Include(c => c.Course)
            .Include(c => c.Teacher) 
            .Include(c => c.Students)
            .ToListAsync();
        ;
        var classViewModels = _mapper.Map<List<ClassViewModel>>(classes);
        return Ok(classViewModels);
    }

    [HttpPost("{classId}/assign-teacher/{teacherId}")]
    public async Task<IActionResult> AssignTeacherToClass(int classId, int teacherId)
    {
        var classObj = await _context.Classes.FindAsync(classId);
        var teacher = await _context.Teachers.FindAsync(teacherId);

        if (classObj == null || teacher == null) return NotFound();

        classObj.Teacher = teacher;
        await _context.SaveChangesAsync();
        return Ok("Teacher assigned successfully!");
    }
    
}
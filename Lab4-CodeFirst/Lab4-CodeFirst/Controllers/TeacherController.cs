using AutoMapper;
using Lab4_CodeFirst.Models;
using Lab4_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeacherController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TeacherController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddTeacher([FromBody] Teachers teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return Ok("Created teacher successfully");
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTeachers()
    {
        var teachers = await _context.Teachers
            .Include(t => t.Classes) 
            .ToListAsync();
        var teacherViewModels = _mapper.Map<List<TeacherViewModel>>(teachers);
        return Ok(teacherViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeacherById(int id)
    {
        var teacher = await _context.Teachers
            .Include(t => t.Classes) 
            .FirstOrDefaultAsync(t => t.TeacherId == id);
        if (teacher == null) return NotFound();
        var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);
        return Ok(teacherViewModel);
    }
}
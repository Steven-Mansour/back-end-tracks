using AutoMapper;
using Lab4_CodeFirst.Models;
using Lab4_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public StudentController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _context.Students
            .Include(s => s.Classes)
            .ThenInclude(c => c.Course)
            .Include(s => s.Classes)
            .ThenInclude(c => c.Teacher)
            .ToListAsync();

        var studentViewModels = _mapper.Map<List<StudentViewModel>>(students);
        return Ok(studentViewModels);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _context.Students
            .Include(s => s.Classes)
            .ThenInclude(c => c.Course)
            .Include(s => s.Classes)
            .ThenInclude(c => c.Teacher)
            .FirstOrDefaultAsync(s => s.StudentId == id);
        if (student == null) return NotFound();
        var studentViewModel = _mapper.Map<StudentViewModel>(student);
        return Ok(studentViewModel);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddStudent([FromBody] Students student)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return Ok("Student created successfully");
    }
    
    [HttpPost("{studentId}/enroll/{classId}")]
    public async Task<IActionResult> EnrollStudentInClass(int studentId, int classId)
    {
        var student = await _context.Students.FindAsync(studentId);
        var classObj = await _context.Classes.FindAsync(classId);
        if (student == null || classObj == null) return NotFound();

        student.Classes.Add(classObj);
        await _context.SaveChangesAsync();
        return Ok("Student enrolled in class successfully!");
    }

    [HttpDelete("{studentId}/remove/{classId}")]
    public async Task<IActionResult> RemoveStudentFromClass(int studentId, int classId)
    {
        var student = await _context.Students
            .Include(s => s.Classes)
            .FirstOrDefaultAsync(s => s.StudentId == studentId);
        if (student == null) return NotFound();

        var classObj = student.Classes
            .FirstOrDefault(c => c.ClassId == classId);
        if (classObj == null) return NotFound();

        student.Classes.Remove(classObj);
        await _context.SaveChangesAsync();
        return Ok("Student removed from class successfully!");
    }
    
}
using DemoLab7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController: ControllerBase
{
    private readonly UniversityDbContext _context;
    public StudentController(UniversityDbContext context)
    {
        _context = context;
    }
    [Authorize(Roles = "teacher")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _context.Students
            .Include(s => s.Classes)
            .ThenInclude(c => c.Course)
            .Include(s => s.Classes)
            .ThenInclude(c => c.Teacher)
            .ToListAsync();
        
        return Ok(students);
    }
    [Authorize(Roles = "teacher")]
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
        return Ok(student);
    }
    [Authorize(Roles = "teacher")]
    [HttpPost("add")]
    public async Task<IActionResult> AddStudent([FromBody] Student student)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return Ok("Student created successfully");
    }
    [Authorize(Roles = "student")]
    [HttpPost("{studentId}/enroll/{classId}")]
    public async Task<IActionResult> EnrollStudentInClass(int studentId, int classId)
    {
        var student = await _context.Students.FindAsync(studentId);
        var classObj = await _context.Classes.FindAsync(classId);
        if (student == null || classObj == null) return NotFound();

        student.Classes.Append(classObj);
        await _context.SaveChangesAsync();
        return Ok("Student enrolled in class successfully!");
    }
    [Authorize(Roles = "student")]
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
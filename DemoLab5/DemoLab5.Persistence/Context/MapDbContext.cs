using DemoLab5.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoLab5.Persistence.Context;

public class MapDbContext : DbContext
{
    
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public MapDbContext(DbContextOptions<MapDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.CourseId });

    }
    
}
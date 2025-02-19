using Microsoft.EntityFrameworkCore;

namespace Lab4_CodeFirst.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Students> Students { get; set; }
    public DbSet<Teachers> Teachers { get; set; }
    public DbSet<Courses> Courses { get; set; }
    public DbSet<Classes> Classes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Students>(entity =>
        {
            entity.HasKey(s => s.StudentId);  
            entity.Property(s => s.FirstName)
                .IsRequired() 
                .HasMaxLength(100); 

            entity.Property(s => s.LastName)
                .IsRequired()  
                .HasMaxLength(100);  

            entity.Property(s => s.Email)
                .IsRequired()  
                .HasMaxLength(255);  

            entity.HasMany(s => s.Classes)
                .WithMany(c => c.Students);
        });
        modelBuilder.Entity<Classes>(entity =>
        {
            entity.HasKey(c => c.ClassId);  
            
            entity.HasOne(c => c.Course)  
                .WithMany(course => course.Classes)  
                .HasForeignKey(c => c.CourseId);  
            
            entity.HasOne(c => c.Teacher)  
                .WithMany(teacher => teacher.Classes)  
                .HasForeignKey(c => c.TeacherId);


            entity.HasMany(c => c.Students)
                .WithMany(student => student.Classes);

        });
        modelBuilder.Entity<Courses>(entity =>
        {
            entity.HasKey(c => c.CourseId); 

            entity.Property(c => c.CourseName)
                .IsRequired()  
                .HasMaxLength(200);  

            entity.HasMany(c => c.Classes)
                .WithOne(cl => cl.Course)  
                .HasForeignKey(cl => cl.CourseId); 
        });
        modelBuilder.Entity<Teachers>(entity =>
        {
            entity.HasKey(t => t.TeacherId);  
            
            entity.HasMany(t => t.Classes)  
                .WithOne(c => c.Teacher)  
                .HasForeignKey(c => c.TeacherId);
        });
       
    }
}
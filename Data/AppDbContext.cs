
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using institute.Entities;
using institute.Interfaces;

namespace institute.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // --------------------------- DbSet ---------------------------
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<TeacherProfile> TeacherProfiles => Set<TeacherProfile>();

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Term> Terms => Set<Term>();
    public DbSet<ClassRoom> ClassRooms => Set<ClassRoom>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<ClassSession> ClassSessions => Set<ClassSession>();
    public DbSet<Attendance> Attendances => Set<Attendance>();

    // --------------------------- Model Configuration ---------------------------
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --------------------------- User & Roles ---------------------------
        modelBuilder.Entity<UserRole>()
            .HasKey(x => new { x.UserId, x.RoleId });

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Teacher" },
            new Role { Id = 3, Name = "Student" }
        );

        // --------------------------- Profiles 1:1 ---------------------------
        modelBuilder.Entity<StudentProfile>()
            .HasKey(s => s.UserId);

        modelBuilder.Entity<StudentProfile>()
            .HasOne(s => s.User)
            .WithOne(u => u.StudentProfile)
            .HasForeignKey<StudentProfile>(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TeacherProfile>()
            .HasKey(t => t.UserId);

        modelBuilder.Entity<TeacherProfile>()
            .HasOne(t => t.User)
            .WithOne(u => u.TeacherProfile)
            .HasForeignKey<TeacherProfile>(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // --------------------------- ClassRoom ---------------------------
        modelBuilder.Entity<ClassRoom>()
            .HasOne(c => c.Teacher)
            .WithMany(t => t.Classes)
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClassRoom>()
            .HasOne(c => c.Course)
            .WithMany()
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClassRoom>()
            .HasOne(c => c.Term)
            .WithMany(t => t.ClassRooms)
            .HasForeignKey(c => c.TermId)
            .OnDelete(DeleteBehavior.Restrict);

        // --------------------------- Enrollment ---------------------------
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.ClassRoomId })
            .IsUnique();

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.ClassRoom)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.ClassRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // --------------------------- ClassSession ---------------------------
        modelBuilder.Entity<ClassSession>()
            .HasOne(s => s.ClassRoom)
            .WithMany(c => c.Sessions)
            .HasForeignKey(s => s.ClassRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // --------------------------- Attendance ---------------------------
        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Session)
            .WithMany(s => s.Attendances)
            .HasForeignKey(a => a.SessionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Student)
            .WithMany(s => s.Attendances)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // --------------------------- Global Soft Delete Filter ---------------------------
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var prop = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                var condition = Expression.Equal(prop, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}

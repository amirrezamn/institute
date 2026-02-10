using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using institute.Entities;
using institute.Interfaces;

namespace institute.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // ======================= DbSets =======================
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
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
    public DbSet<Grade> Grades => Set<Grade>();


    // ======================= Model Config =======================
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------- UserRole (Many-to-Many) ----------
        modelBuilder.Entity<UserRole>()
            .HasKey(x => new { x.UserId, x.RoleId });

        // ---------- User ----------
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // ---------- Seed Roles ----------
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Teacher" },
            new Role { Id = 3, Name = "Student" }
        );

        // ---------- StudentProfile (1:1 with User) ----------
        modelBuilder.Entity<StudentProfile>()
            .HasKey(s => s.UserId);

        modelBuilder.Entity<StudentProfile>()
            .HasOne(s => s.User)
            .WithOne(u => u.StudentProfile)
            .HasForeignKey<StudentProfile>(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---------- TeacherProfile (1:1 with User) ----------
        modelBuilder.Entity<TeacherProfile>()
            .HasKey(t => t.UserId);

        modelBuilder.Entity<TeacherProfile>()
            .HasOne(t => t.User)
            .WithOne(u => u.TeacherProfile)
            .HasForeignKey<TeacherProfile>(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---------- ClassRoom ----------
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

        // ---------- Enrollment ----------
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

        // ---------- ClassSession ----------
        modelBuilder.Entity<ClassSession>()
            .HasOne(s => s.ClassRoom)
            .WithMany(c => c.Sessions)
            .HasForeignKey(s => s.ClassRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---------- Attendance ----------
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
        // ---------- PasswordResetToken ---------- 
            modelBuilder.Entity<PasswordResetToken>()
            .HasOne(t => t.User)
            .WithMany() // لازم نیست navigation تو User بذاری
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PasswordResetToken>()
            .HasIndex(t => t.Token)
            .IsUnique();
        // ----------Grade----------
        modelBuilder.Entity<Grade>()
            .HasIndex(g => new { g.StudentId, g.ClassRoomId })
            .IsUnique();

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.ClassRoom)
            .WithMany()
            .HasForeignKey(g => g.ClassRoomId)
            .OnDelete(DeleteBehavior.Restrict);
        

        // ---------- Global Soft Delete ----------
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                var condition = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);

                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(lambda);
            }
        }
    }
}

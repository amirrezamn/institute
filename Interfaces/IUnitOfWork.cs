using institute.Entities;
using institute.Services;

namespace institute.Interfaces;

public interface IUnitOfWork:IDisposable
{
    IRepository<StudentProfile>  StudentProfiles { get; }
    IRepository<TeacherProfile>  TeacherProfiles { get; }
    IRepository<User>  Users { get; }
    IRepository<AdminProfile>  AdminProfiles { get; }
    IRepository<Role>  Roles { get; }
    IRepository<UserRole>  UserRoles { get; }
    IRepository<Course>  Courses { get; }
    IRepository<Term>  Terms { get; }
    IRepository<ClassRoom>  ClassRooms { get; }
    IRepository<Enrollment>  Enrollments { get; }
    IRepository<Attendance>  Attendances { get; }
    IRepository<ClassSession>  ClassSessions { get; }
    
    Task<int> CompleteAsync();  

}
using institute.Data;
using institute.Entities;
using institute.Interfaces;
using institute.Services;

namespace institute.Repositories;

public class UnitOfWork : IUnitOfWork
{

    public IRepository<StudentProfile> StudentProfiles { get; }
    public IRepository<TeacherProfile> TeacherProfiles { get; }
    public IRepository<User> Users { get; }
    public IRepository<AdminProfile> AdminProfiles { get; }
    public IRepository<Role> Roles { get; }
    public IRepository<UserRole> UserRoles { get; }
    public IRepository<Course> Courses { get; }
    public  IRepository<Term> Terms { get; }
    public IRepository<ClassRoom> ClassRooms { get; }
    public IRepository<Enrollment> Enrollments { get; }
    public IRepository<Attendance> Attendances { get; }
    public IRepository<ClassSession>  ClassSessions { get; }
    
   
    
    private readonly AppDbContext _Context;
    

    public UnitOfWork(AppDbContext context)
    {
        _Context = context;
        Users=new GenericRepository<User>(_Context);
        TeacherProfiles=new GenericRepository<TeacherProfile>(_Context);
        AdminProfiles=new GenericRepository<AdminProfile>(_Context);
        StudentProfiles=new GenericRepository<StudentProfile>(_Context);
        Roles=new GenericRepository<Role>(_Context);
        UserRoles=new GenericRepository<UserRole>(_Context);
        Courses=new GenericRepository<Course>(_Context);
        Terms=new GenericRepository<Term>(_Context);
        ClassRooms=new GenericRepository<ClassRoom>(_Context);
        Enrollments=new GenericRepository<Enrollment>(_Context);
        Attendances=new GenericRepository<Attendance>(_Context);
        ClassSessions=new GenericRepository<ClassSession>(_Context);
      
    }

    public async Task<int> CompleteAsync()
    {
        return await _Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _Context.Dispose();
    }
}
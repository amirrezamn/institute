using System.Linq.Expressions;
using institute.DTOs;
using institute.DTOs.Admin;
using institute.Entities;
using institute.Helpers;
using institute.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace institute.Services;

public class AdminService:IAdminService
{
    private IUnitOfWork _unitOfWork;

    public AdminService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    #region Student

    public async Task AddStudentAsync(CreateStudentDto dto)
    {
        // Add student
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), 
        };
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        var studentProfile = new StudentProfile
        {
            UserId =  user.Id,
            Level =   dto.Leve
        };
        await _unitOfWork.StudentProfiles.AddAsync(studentProfile);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateStudentAsync(UpdateStudentDto dto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.FullName = dto.FullName;
        user.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }
        var profile = await _unitOfWork.StudentProfiles.GetByIdAsync(user.Id);
        profile.Level = dto.Leve;
        profile.PhoneNumber = dto.PhoneNumber;
       await  _unitOfWork.StudentProfiles.UpdateAsync(profile);
       await  _unitOfWork.CompleteAsync();
    }

    public async Task SoftDeleteStudentAsync(int userId)
    {
        var student= await _unitOfWork.Users.GetByIdAsync(userId);
        if (student == null)
        {
            throw new Exception("User not found");
        }
        await _unitOfWork.Users.SoftDeleteAsync(student);
        await _unitOfWork.CompleteAsync();
    }
    
    public async Task HardDeleteStudentAsync(int userId)
    {
        var student= await _unitOfWork.Users.GetByIdAsync(userId);
        if (student == null)
        {
            throw new Exception("User not found");
        }
        await _unitOfWork.Users.HardDeleteAsync(student);
        await _unitOfWork.CompleteAsync();
    }
    

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _unitOfWork.StudentProfiles.GetAllAsync();
        return students.Select(s=>new StudentDto()
        {
            FullName  = s.User.FullName,
            Email = s.User.Email,
            Level = s.Level,
            PhoneNumber = s.PhoneNumber
        }).ToList();
    }


    #endregion

    #region Teacher

    public async Task AddTeacherAsync(CreateTeacherDto dto)
    {
        //Add teacher
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            
        };
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        var teacherProfile = new TeacherProfile
        {
            UserId = user.Id,
            ExperienceYears =  dto.ExperienceYears,
            Speciality =   dto.Speciality,
            PhoneNumber = dto.PhoneNumber
        };
        await _unitOfWork.TeacherProfiles.AddAsync(teacherProfile);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateTeacherAsync(UpdateTeacherDto dto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.FullName = dto.FullName;
        user.Email = dto.Email;
        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }
     
        var profile = await _unitOfWork.TeacherProfiles.GetByIdAsync(user.Id);
        profile.ExperienceYears = dto.ExperienceYears;
        profile.PhoneNumber = dto.PhoneNumber;
        profile.Speciality = dto.Speciality;
        
    }

    public async Task SoftDeleteTeacherAsync(int userId)
    {
        var user= await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await _unitOfWork.Users.SoftDeleteAsync(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HardDeleteTeacherAsync(int userId)
    {
        var user= await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await _unitOfWork.Users.HardDeleteAsync(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<TeacherDto>> GetAllTeachersAsync()
    {
        var teachers = await _unitOfWork.TeacherProfiles.GetAllAsync();
        return teachers.Select(t => new TeacherDto()
        {
            FullName = t.User.FullName,
            Email = t.User.Email,   
            PhoneNumber = t.PhoneNumber,
            Speciality = t.Speciality,
            ExperienceYears = t.ExperienceYears
        }).ToList();
    }
    #endregion

    #region Admin

    public async Task AddAdminAsync(CreateAdminDto dto)
    {
        //Add admin
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        };
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        var adminProfile = new AdminProfile
        {
            UserId = user.Id,
            Department =   dto.Department
        };
        await _unitOfWork.AdminProfiles.AddAsync(adminProfile);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAdminAsync(UpdateAdminDto dto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw  new Exception("User not found");
        }
        user.FullName = dto.FullName;
        user.Email = dto.Email;
        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }
        var profile = await _unitOfWork.AdminProfiles.GetByIdAsync(user.Id);
        profile.Department= dto.Department;
        profile.PhoneNumber = dto.Department;
        
        await _unitOfWork.AdminProfiles.UpdateAsync(profile);
        await  _unitOfWork.CompleteAsync();

    }

    public async Task SoftDeleteAdminAsync(int userId)
    {
        var user= await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await _unitOfWork.Users.SoftDeleteAsync(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HardDeleteAdminAsync(int userId)
    {
        var user= await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await _unitOfWork.Users.HardDeleteAsync(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task <List<AdminDto>> GetAllAdminsAsync()
    {
        var admins = await _unitOfWork.AdminProfiles.GetAllAsync();
        return admins.Select(x => new AdminDto()
        {
            FullName = x.FullName,
            Department = x.Department,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            IsDeleted = false
        }).ToList();
    }

    #endregion

    #region Role

    public async Task AddRoleAsync(CreateRoleDto dto)
    {
        //Add role
        var role = new Role
        {
            Name = dto.Name,
        };
        await _unitOfWork.Roles.AddAsync(role);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateRoleAsync(UpdateRoleDto dto)
    {
        var role=await _unitOfWork.Roles.GetByIdAsync(dto.RoleId);
        if (role == null)
        {
            throw  new Exception("Role not found");
        }
        role.Name = dto.Name;
        await _unitOfWork.Roles.UpdateAsync(role);
        await _unitOfWork.CompleteAsync();
    }

    public async Task SoftDeleteRoleAsync(int roleId)
    {
        var role= await _unitOfWork.Roles.GetByIdAsync(roleId);
        if (role == null)
        {
            throw  new Exception("Role not found");
        }
        await _unitOfWork.Roles.SoftDeleteAsync(role);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HardDeleteRoleAsync(int roleId)
    {
        var role= await _unitOfWork.Roles.GetByIdAsync(roleId);
        if (role == null)
        {
            throw  new Exception("Role not found");
        }
        await _unitOfWork.Roles.HardDeleteAsync(role);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();
        return roles.Select(x =>new RoleDto()
        {
            Name = x.Name
        }).ToList();
    }
    #endregion
    public async Task AssignRoleToUserAsync(AssignRoleToUserDto dto)
    {
        var  user = await _unitOfWork.Users.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
       var role = await _unitOfWork.Roles.GetByIdAsync(dto.RoleId);
       if (role == null)
       {
           throw  new Exception("Role not found");
       }
       var exists= (await _unitOfWork.UserRoles.FindAsync(ur => ur.UserId == dto.UserId && ur.RoleId == role.Id)).Any();

       if (exists)
       {
           return;
       }
        var userRole =  new UserRole
        {
            UserId =  dto.UserId,
            RoleId =  dto.RoleId
        };
        await _unitOfWork.UserRoles.AddAsync(userRole);
        await _unitOfWork.CompleteAsync();
    }

    public async Task SoftDeleteUserAsync(int userId)
    {
        var user= await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            {
            throw new Exception("User not found");
            }
        await _unitOfWork.Users.SoftDeleteAsync(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HardDeleteUserAsync(int userId)
    {
        var user= await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await _unitOfWork.Users.HardDeleteAsync(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        var roles =await _unitOfWork.Roles.GetAllAsync();
        var userroles =await _unitOfWork.UserRoles.GetAllAsync();
        var result = new List<UserDto>();
        foreach (var user in users)
        {
            var roleIds = userroles.Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId).ToList();
            var roleName = roles.Where(r => roleIds.Contains(r.Id))
                .Select(r => r.Name).ToList();
            result.Add(new UserDto
            {
                Id =  user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Roles = roleName,
            });
        }
        return result;
    }

    #region Course

    public async Task CreateCourseAsync(CreateCourseDto dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            Level = dto.Level,
            IsDeleted = false

        };
        await _unitOfWork.Courses.AddAsync(course);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateCourseAsync(UpdateCourseDto dto)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(dto.CourseId);
        if (course == null)
        {
            throw  new Exception("Course not found");
        }
        course.Title = dto.Title;
        course.Level = dto.Level;
        await _unitOfWork.Courses.UpdateAsync(course);
        await _unitOfWork.CompleteAsync();
    }

    public async Task SoftDeleteCourseAsync(int courseId)
    {
        var Course= await _unitOfWork.Courses.GetByIdAsync(courseId);
        if (Course == null)
        {
            return;
        } 
        await _unitOfWork.Courses.SoftDeleteAsync(Course);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HardDeleteCourseAsync(int courseId)
    {
        var course= await _unitOfWork.Courses.GetByIdAsync(courseId);
        if (course == null)
        {
            return;
        }
        await _unitOfWork.Courses.HardDeleteAsync(course);
        await _unitOfWork.CompleteAsync();
    }

    public async Task <List<CourseListDto>> GetAllCoursesAsync()
    {
        var courses = await _unitOfWork.Courses.GetAllAsync();
        return courses.Select(c=> new CourseListDto()
        {
            Title   =  c.Title,
            Level =  c.Level
        } ).ToList();
    }

    #endregion

    #region Term

    public async Task CreateTermAsync(CreateTermDto dto)
    {
        var term = new Term
        {
            Title = dto.Title,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
        };
        await _unitOfWork.Terms.AddAsync(term);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateTermAsync(UpdateTermDto dto)
    {
        var term = await _unitOfWork.Terms.GetByIdAsync(dto.TermId);
        if (term == null)
        {
            throw new Exception("Term not found");
        }
        term.Title = dto.Title;
        term.StartDate = dto.StartDate;
        term.EndDate = dto.EndDate;
        await _unitOfWork.Terms.UpdateAsync(term);
        await _unitOfWork.CompleteAsync();
    }

    public async Task SoftDeleteTermAsync(int termId)
    {
        var term= await _unitOfWork.Terms.GetByIdAsync(termId);
        if (term == null)
        {
            throw new Exception("Term not found");
        }
        await _unitOfWork.Terms.SoftDeleteAsync(term);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HardDeleteTermAsync(int termId)
    {
        var term= await _unitOfWork.Terms.GetByIdAsync(termId);
        if (term == null)
        {
            throw new Exception("Term not found");
        }
        await _unitOfWork.Terms.HardDeleteAsync(term);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<TermDto>> GetAllTermsAsync()
    {
        var terms = await _unitOfWork.Terms.GetAllAsync();
        return terms.Select(t=> new TermDto()
        {
            Title =   t.Title,
            StartDate = t.StartDate,
            EndDate = t.EndDate
        }).ToList();
    }

    #endregion

    #region ClassRoom

    public async Task CreateClassRoomAsync(CreateClassRoomDto dto)
    {
        var classroom = new ClassRoom
        {
            
            CourseId = dto.CourseId,
            TermId =  dto.TermId,
            TeacherId =  dto.TeacherId
        };
        await _unitOfWork.ClassRooms.AddAsync(classroom);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateClassRoomAsync(UpdateClassRoomDto dto)
    {
        var classroom = await _unitOfWork.ClassRooms.GetByIdAsync(dto.ClassroomId);
        if (classroom == null)
        {
            throw new Exception("Classroom not found");
        }
        classroom.CourseId= dto.CourseId;
        classroom.TermId= dto.TermId;
        classroom.TeacherId= dto.TeacherId;
        await _unitOfWork.ClassRooms.UpdateAsync(classroom);
        await _unitOfWork.CompleteAsync();
        
    }

    public async Task SoftDeleteClassRoomAsync(int classroomId)
    {
        var classroom = await _unitOfWork.ClassRooms.GetByIdAsync(classroomId);
        if (classroom == null)
        {
            throw new Exception("Classroom not found");
        }
        await _unitOfWork.ClassRooms.SoftDeleteAsync(classroom);
        await _unitOfWork.CompleteAsync();
    }

    public async Task HardDeleteClassRoomAsync(int classroomId)
    {
        var classroom = await _unitOfWork.ClassRooms.GetByIdAsync(classroomId);
        if (classroom == null)
        {
            throw new Exception("Classroom not found");
        }
        await _unitOfWork.ClassRooms.HardDeleteAsync(classroom);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<ClassRoomDto>> GetAllClassRoomsAsync()
    {
        var classrooms = await _unitOfWork.ClassRooms.GetAllAsync();
        return classrooms.Select(cr => new ClassRoomDto()
        {
            Capacity = cr.Capacity,
            CourseId = cr.CourseId,
            TermId = cr.TermId,
            TeacherId = cr.TeacherId,
            TeacherName = cr.Teacher,
            CourseTitle = cr.Course,
            TermName = cr.Term 
            

        }).ToList();
    }

    #endregion

    #region ََََEnrollment

    public async Task EnrollStudentAsync(EnrollStudentDto dto)
    {
        var student=_unitOfWork.StudentProfiles.GetByIdAsync(dto.StudentId);
        if (student == null)
        {
            throw new Exception("Student not found");
        }
        var classroom = await _unitOfWork.ClassRooms.GetByIdAsync(dto.ClassRoomId);
        if (classroom == null)
        {
            throw new Exception("Classroom not found");
        }

        var exist = (await _unitOfWork.Enrollments
            .FindAsync(e=>e.StudentId==dto.StudentId && e.ClassRoomId==dto.ClassRoomId ))
            .Any();
        if (exist)
        {
            throw new Exception ("Student already exists");
        }

        var enrollCount = (await _unitOfWork.Enrollments
            .FindAsync(ec=>ec.ClassRoomId==dto.ClassRoomId)).Count;
        if (enrollCount>=classroom.Capacity)
        {
            throw new Exception("ClassRoom is Full");
        }

        var enrolment = new Enrollment
        {
            StudentId =  dto.StudentId,
            ClassRoomId =  dto.ClassRoomId
        };
        await _unitOfWork.Enrollments.AddAsync(enrolment);
        await _unitOfWork.CompleteAsync();
    }
    public async Task SoftDeleteEnrollmentAsync(int enrollmentId)
    {
        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollmentId);
        if (enrollment == null)
            {
            throw new Exception("Enrollment not found");
            }
        await _unitOfWork.Enrollments.SoftDeleteAsync(enrollment);
        await _unitOfWork.CompleteAsync();
    }
    public async Task HardDeleteEnrollmentAsync(int enrollmentId)
    {
        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollmentId);
        if (enrollment == null)
            {
            throw new Exception("Enrollment not found");
            }
        await _unitOfWork.Enrollments.HardDeleteAsync(enrollment);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<List<StudentClassDto>> GetAllEnrollmentsAsync()
    {
        var enrollment= await _unitOfWork.Enrollments.GetAllAsync();
        return enrollment.Select(e=>new StudentClassDto()
        {
            ClassRoomId=e.ClassRoomId,
            TeacherName =e.ClassRoom.Teacher.User.FullName,
            TermTitle = e.ClassRoom.Term.Title,
            CourseTitle =e.ClassRoom.Course.Title,
            
            
        }).ToList();
    }
    #endregion

    #region Pagination

    public async Task<PagedResult<PageStudentDto>> PaginationStudents(PaginationRequestDto dto)
    { 
        Expression<Func<StudentProfile, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            filter=s=>s.User.Email.Contains(dto.Search)|| s.User.FullName.Contains(dto.Search);
        }
        var orderBy = ExpressionHelper.BuildOrderBy<StudentProfile>(dto.SortBy);

        var page=await _unitOfWork.StudentProfiles.Pagination(       
            pageNumber: dto.PageNumber,
            pageSize: dto.PageSize,
            orderBy: orderBy,
            ascending: dto.Ascending,
            filter: filter,
            includes: s => s.User
           );
        return new PagedResult<PageStudentDto>
        {
            Items = page.Items.Select(s => new PageStudentDto
            {
                FullName = s.User.FullName,
                Email = s.User.Email,
                Level = s.Level,
                PhoneNumber = s.PhoneNumber
            }).ToList(),
            TotalCount = page.TotalCount,
            PageSize = page.PageSize,
            PageNumber = page.PageNumber,
        };
    }

    public async Task<PagedResult<TeacherPageDto>> PaginationTeachers(PaginationRequestDto dto)
    {
       Expression<Func<TeacherProfile, bool>>? filter = null;
       if (!string.IsNullOrWhiteSpace(dto.Search))
       {
           filter = t => t.User.FullName.Contains(dto.Search)|| t.User.Email.Contains(dto.Search);
       }
       var orderBy = ExpressionHelper.BuildOrderBy<TeacherProfile>(dto.SortBy);
       var page = await _unitOfWork.TeacherProfiles.Pagination(
           dto.PageNumber,
           dto.PageSize,
           orderBy,
           dto.Ascending,
           filter,
           t => t.User
       );
       return new PagedResult<TeacherPageDto>
       {
           Items = page.Items.Select(t => new TeacherPageDto
           {
               FullName = t.User.FullName,
               Email = t.User.Email,
               PhoneNumber = t.PhoneNumber,
               ExperienceYears = t.ExperienceYears,
               Speciality = t.Speciality,
               ClassesCount = t.Classes.Count
           }).ToList(),
           PageNumber = page.PageNumber,
           PageSize = page.PageSize,
           TotalCount = page.TotalCount,
       };
    }

    public async Task<PagedResult<AdminPageDto>> PaginationAdmins(PaginationRequestDto dto)
    {
        Expression<Func<AdminProfile, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            filter = t => t.User.FullName.Contains(dto.Search)|| t.User.Email.Contains(dto.Search);
        }
        var orderBy = ExpressionHelper.BuildOrderBy<AdminProfile>(dto.SortBy);
        var page = await _unitOfWork.AdminProfiles.Pagination(dto.PageNumber, dto.PageSize, orderBy,dto.Ascending, filter,t=>t.User);
      
        
        return new PagedResult<AdminPageDto>
        {
            Items = page.Items.Select(a=>new AdminPageDto
            {
                Department = a.Department,
                Email = a.Email,
                FullName = a.FullName,
                PhoneNumber = a.PhoneNumber,
            }).ToList(),
            PageNumber = page.PageNumber,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount
        };
    }

    public async Task<PagedResult<ClassRoomPageDto>> PaginationClassRooms(PaginationRequestDto dto)
    {
        Expression<Func<ClassRoom,bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            filter=t=>t.Name.ToLower().Contains(dto.Search.ToLower())|| t.Teacher.User.FullName.Contains(dto.Search.ToLower());
        }
        var sortBy = ExpressionHelper.BuildOrderBy<ClassRoom>(dto.SortBy);
        var page = await _unitOfWork.ClassRooms.Pagination(dto.PageNumber,dto.PageSize,sortBy,dto.Ascending,filter,t=>t.Teacher,t=>t.Course,t=>t.Term);
        return new PagedResult<ClassRoomPageDto>
        {
            Items = page.Items.Select(c => new ClassRoomPageDto
            {
                CourseId = c.CourseId,
                Capacity = c.Capacity,
                Name = c.Name,
                TeacherId = c.TeacherId,
                TermId = c.TermId
            }).ToList(),
            PageNumber = page.PageNumber,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount,
        };
    }

    public async Task<PagedResult<ClassSessionPageDto>> PaginationClassSession(PaginationRequestDto dto)
    {
        Expression<Func<ClassSession,bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            filter=t=>t.Id.ToString().Contains(dto.Search)||t.SessionDate.Day.ToString().Contains(dto.Search);
        }
        var sortBy = ExpressionHelper.BuildOrderBy<ClassSession>(dto.SortBy);
        var page = await _unitOfWork.ClassSessions.Pagination(dto.PageNumber,dto.PageSize,sortBy,dto.Ascending,filter,t=>t.Attendances,t=>t.ClassRoom);
        return new PagedResult<ClassSessionPageDto>
        {
            Items = page.Items.Select(s => new ClassSessionPageDto
            {
                SessionDate = s.SessionDate,
                ClassRoomId = s.ClassRoomId
            }).ToList(),
            PageNumber = page.PageNumber,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount,
        };
    }

    public async Task<PagedResult<TermPageDto>> PaginationTerm(PaginationRequestDto dto)
    {
        Expression<Func<Term,bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            filter=t=>t.Title.ToLower().Contains(dto.Search.ToLower());
        }
        var sortBy = ExpressionHelper.BuildOrderBy<Term>(dto.SortBy);
       var page=await _unitOfWork.Terms.Pagination(dto.PageNumber,dto.PageSize,sortBy,dto.Ascending,filter,t=>t.ClassRooms);
       return new PagedResult<TermPageDto>
       {
           Items = page.Items.Select(s => new TermPageDto
           {
               EndDate = s.EndDate,
               StartDate = s.StartDate
           }).ToList(),
           PageNumber = page.PageNumber,
           PageSize = page.PageSize,
           TotalCount = page.TotalCount,
       };
    }

    public async Task<PagedResult<AttendancePageDto>> PaginationAttendance(PaginationRequestDto dto)
    {
        Expression<Func<Attendance,bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            filter=a=> a.Student.User.FullName.Contains(dto.Search)
                       || a.Student.User.Email.Contains(dto.Search);
        }
        var sortBy = ExpressionHelper.BuildOrderBy<Attendance>(dto.SortBy);
        var page = await _unitOfWork.Attendances.Pagination(dto.PageNumber, dto.PageSize, sortBy, dto.Ascending, filter,
            t => t.Session, t => t.Student,t=>t.Student.User);
        return new PagedResult<AttendancePageDto>
        {
            Items = page.Items.Select(s => new AttendancePageDto
            {
                StudentId = s.StudentId,
                StudentName = s.Student.User.FullName,
                Status = s.Status
            }).ToList(),
            PageNumber = page.PageNumber,
            PageSize = page.PageSize,
            TotalCount = page.TotalCount,
        };
    }
    #endregion

    #region Attendance

    public async Task MarkAttendanceAsync(MarkAttendanceDto dto)
    {
        var exist= (await _unitOfWork.Attendances.FindAsync(a=>a.SessionId==dto.ClassSessionId && a.StudentId==dto.StudentId)).Any();
        if (exist)
        {
            throw new Exception("Attendance already Marked");
        }

        var attendance = new Attendance
        {
            StudentId = dto.StudentId,
            SessionId = dto.ClassSessionId,
            Status = dto.Status,
        };
        await _unitOfWork.Attendances.AddAsync(attendance);
        await _unitOfWork.CompleteAsync();
    }

    #endregion
}
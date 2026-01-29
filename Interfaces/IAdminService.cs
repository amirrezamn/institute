using System.Linq.Expressions;
using institute.DTOs;
using institute.DTOs.Admin;
using institute.Entities;

namespace institute.Interfaces;

public interface IAdminService
{
    #region Student

    Task AddStudentAsync(CreateStudentDto dto);
    Task UpdateStudentAsync(UpdateStudentDto dto);
    Task SoftDeleteStudentAsync(int userId);
    Task HardDeleteStudentAsync(int userId);
    Task<List<StudentDto>> GetAllStudentsAsync();

    #endregion

    #region Teacher

    Task AddTeacherAsync(CreateTeacherDto dto);
    Task UpdateTeacherAsync(UpdateTeacherDto dto);
    Task SoftDeleteTeacherAsync(int userId);
    Task HardDeleteTeacherAsync(int userId);
    Task<List<TeacherDto>> GetAllTeachersAsync();

    #endregion

    #region Admin

    Task AddAdminAsync(CreateAdminDto dto);
    Task UpdateAdminAsync(UpdateAdminDto dto);
    Task SoftDeleteAdminAsync(int userId);
    Task HardDeleteAdminAsync(int userId);
    Task<List<AdminDto>> GetAllAdminsAsync();

    #endregion

    #region Role

    Task AddRoleAsync(CreateRoleDto dto);
    Task UpdateRoleAsync(UpdateRoleDto dto);
    Task SoftDeleteRoleAsync(int roleId);
    Task HardDeleteRoleAsync(int roleId);
    Task<List<RoleDto>> GetAllRolesAsync();

    #endregion

    #region UserRole

    Task AssignRoleToUserAsync(AssignRoleToUserDto dto);

    #endregion

    #region User

    Task SoftDeleteUserAsync(int userId);
    Task HardDeleteUserAsync(int userId);
    Task<List<UserDto>> GetAllUsersAsync();

    #endregion

    #region Course

    Task CreateCourseAsync(CreateCourseDto dto);
    Task UpdateCourseAsync(UpdateCourseDto dto);
    Task SoftDeleteCourseAsync(int courseId);
    Task HardDeleteCourseAsync(int courseId);
    Task<List<CourseListDto>> GetAllCoursesAsync();

    #endregion

    #region Term

    Task CreateTermAsync(CreateTermDto dto);
    Task UpdateTermAsync(UpdateTermDto dto);
    Task SoftDeleteTermAsync(int termId);
    Task HardDeleteTermAsync(int termId);
    Task<List<TermDto>> GetAllTermsAsync();

    #endregion

    #region ClassRoom

    Task CreateClassRoomAsync(CreateClassRoomDto dto);
    Task UpdateClassRoomAsync(UpdateClassRoomDto dto);
    Task SoftDeleteClassRoomAsync(int classroomId);
    Task HardDeleteClassRoomAsync(int classroomId);
    Task<List<ClassRoomDto>> GetAllClassRoomsAsync();

    #endregion

    #region Enrollment

    Task EnrollStudentAsync(EnrollStudentDto dto);
    Task SoftDeleteEnrollmentAsync(int enrollmentId);
    Task HardDeleteEnrollmentAsync(int enrollmentId);
    Task<List<StudentClassDto>> GetAllEnrollmentsAsync();

    Task<PagedResult<PageStudentDto>> PaginationStudents(PaginationRequestDto dto);

    Task<PagedResult<TeacherPageDto>> PaginationTeachers(PaginationRequestDto dto);

    Task<PagedResult<AdminPageDto>> PaginationAdmins(PaginationRequestDto dto);

    Task<PagedResult<ClassRoomPageDto>> PaginationClassRooms(PaginationRequestDto dto);


    Task<PagedResult<ClassSessionPageDto>> PaginationClassSession(PaginationRequestDto dto);

    Task<PagedResult<TermPageDto>> PaginationTerm(PaginationRequestDto dto);
    Task<PagedResult<AttendancePageDto>> PaginationAttendance(PaginationRequestDto dto);
    Task MarkAttendanceAsync(MarkAttendanceDto dto);

    #endregion

}
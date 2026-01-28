using System.Linq.Expressions;
using institute.DTOs;
using institute.DTOs.Admin;
using institute.Entities;
using institute.Interfaces;
using institute.Services;
using Microsoft.AspNetCore.Mvc;

namespace institute.Controllers;
[ApiController]
[Route("api/")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {

        _adminService = adminService;
    }

    #region Student

    [HttpPost("student")]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto dto)
    {
        await _adminService.AddStudentAsync(dto);
        return Ok("Student created successfully");
    }
    [HttpPost("studentP")]
    public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDto dto)
    {
        await _adminService.UpdateStudentAsync(dto);
        return Ok("Student updated successfully");
    }
    [HttpDelete("student/{StudentId}")]
    public async Task<IActionResult> SoftDeleteStudent([FromRoute] int studentId)
    {
        await _adminService.SoftDeleteStudentAsync(studentId);
        return Ok("Student SoftDeleted successfully");
    }

    [HttpDelete("student/{StudentId}/hard")]
    public async Task<IActionResult> HardDeleteStudent([FromRoute] int studentId)
    {
        await  _adminService.HardDeleteStudentAsync(studentId);
        return Ok("Student HardDeleted successfully");
    }

    [HttpGet("student")]
    public async Task<IActionResult> GetAllStudent()
    {
       var  result=await _adminService.GetAllStudentsAsync();
        return Ok(result);
    }

    #endregion

    #region Teacher

    [HttpPost("teacher")]
    public async Task<IActionResult> AddTeacher([FromBody] CreateTeacherDto dto)
    {
        await _adminService.AddTeacherAsync(dto);
        return Ok("Teacher added successfully");
    }
    [HttpPost("teacherP")]
    public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeacherDto dto)
    {
        await _adminService.UpdateTeacherAsync(dto);
        return Ok("Teacher Updated successfully");
    }
    [HttpDelete("teacher/{TeacherId}")]
    public async Task<IActionResult> SoftDeleteTeacher([FromRoute] int teacherId)
    {
        await _adminService.SoftDeleteTeacherAsync(teacherId);
        return Ok("Teacher SoftDeleted successfully");
    }

    [HttpDelete("teacher/{TeacherId}/hard")]
    public async Task<IActionResult> HardDeleteTeacher([FromRoute] int teacherId)
    {
        await _adminService.HardDeleteTeacherAsync(teacherId);
        return Ok("Teacher HardDeleted successfully");
    }

    [HttpGet("teacher")]
    public async Task<IActionResult> GetAllTeachers()
    {
        var result = await _adminService.GetAllTeachersAsync();
        return Ok(result);
    }

    #endregion

    #region Admin

    [HttpPost("admin")]
    public async Task<IActionResult> AddAdmin([FromBody] CreateAdminDto dto)
    {
        await _adminService.AddAdminAsync(dto);
        return Ok("Admin created successfully");
    }
    [HttpPost("adminP")]
    public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminDto dto)
    {
        await _adminService.UpdateAdminAsync(dto);
        return Ok("Admin Updated successfully");
    }

    [HttpDelete("admin/{AdminId}")]
    public async Task<IActionResult> SoftDeleteAdmin([FromRoute] int adminId)
    {
        await _adminService.SoftDeleteAdminAsync(adminId);
        return Ok("Admin SoftDeleted successfully");
    }

    [HttpDelete("admin/{AdminId}/hard")]
    public async Task<IActionResult> HardDeleteAdmin([FromRoute] int adminId)
    {
        await _adminService.HardDeleteAdminAsync(adminId);
        return Ok("Admin HardDeleted successfully");
    }

    [HttpGet("admin")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var result = await _adminService.GetAllAdminsAsync();
        return Ok(result);
    }

    #endregion

    #region Term

    [HttpPost("term")]
    public async Task<IActionResult> CreateTerm([FromBody] CreateTermDto dto)
    {
        await _adminService.CreateTermAsync(dto);
        return Ok("Term created successfully");
    }
    [HttpPost("termP")]
    public async Task<IActionResult> UpdateTerm([FromBody] UpdateTermDto dto)
    {
        await _adminService.UpdateTermAsync(dto);
        return Ok("Term Updated successfully");
    }
    [HttpDelete("term/{TermId}")]
    public async Task<IActionResult> SoftDeleteTerm([FromRoute] int termId)
    {
        await _adminService.SoftDeleteTermAsync(termId);
        return Ok("Term SoftDeleted successfully");
    }
    [HttpDelete("term/{TermId}/hard")]
    public async Task<IActionResult> HardDeleteTerm([FromRoute] int termId)
    {
        await _adminService.HardDeleteTermAsync(termId);
        return Ok("Term HardDeleted successfully");
    }
    [HttpGet("term")]
    public async Task<IActionResult> GetAllTerms()
    {
        var result= await _adminService.GetAllTermsAsync();
        return Ok(result);
    }

    #endregion

    #region Role

    [HttpPost("role")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
    {
        await _adminService.AddRoleAsync(dto);
        return Ok("Role created successfully");
    }
    [HttpPost("roleP")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto dto)
    {
        await _adminService.UpdateRoleAsync(dto);
        return Ok("Role Updated successfully");
    }

    [HttpPost("role/{RoleId}")]
    public async Task<IActionResult> SoftDeleteRole([FromBody] int roleId)
    {
        await _adminService.SoftDeleteRoleAsync(roleId);
        return Ok("Role SoftDeleted successfully");
    }

    [HttpPost("role/{RoleId}/hard")]
    public async Task<IActionResult> HardDeleteRole([FromBody] int roleId)
    {
        await _adminService.HardDeleteRoleAsync(roleId);
        return Ok("Role HardDeleted successfully");
    }

    [HttpGet("role")]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var result = await _adminService.GetAllRolesAsync();
        return Ok(result);
    }

    #endregion

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserDto dto)
    {
        await  _adminService.AssignRoleToUserAsync(dto);
        return Ok("Assign Role To User successfully");
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _adminService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpDelete("user/{UserId}")]
    public async Task<IActionResult> SoftDeleteUserAsync([FromRoute] int userId)
    {
        await _adminService.SoftDeleteUserAsync(userId);
        return Ok("User SoftDeleted successfully");
    }

    [HttpDelete("user/{UserId}/hard")]
    public async Task<IActionResult> HardDeleteUserAsync([FromRoute] int userId)
    {
        await _adminService.HardDeleteUserAsync(userId);
        return Ok("User HardDeleted successfully");
    }

    #region Course


    [HttpPost("course")]
    public async Task<IActionResult> CreateCourseAsync([FromBody] CreateCourseDto dto)
    {
        await _adminService.CreateCourseAsync(dto);
        return Ok("course created successfully");
    }
    [HttpPost("courseP")]
    public async Task<IActionResult> UpdateCourseAsync([FromBody] UpdateCourseDto dto)
    {
        await _adminService.UpdateCourseAsync(dto);
        return Ok("Course Updated successfully");
    }
    [HttpDelete("course/{CourseId}")]
    public async Task<IActionResult> SoftDeleteCourseAsync([FromRoute] int courseId)
    {
        await _adminService.SoftDeleteCourseAsync(courseId);
        return Ok("course SoftDeleted successfully");

    }

    [HttpDelete("course/{CourseId}/hard")]
    public async Task<IActionResult> HardDeleteCourseAsync([FromRoute] int courseId)
    {
        await _adminService.HardDeleteCourseAsync(courseId);
        return Ok("course HardDeleted successfully");
    }

    [HttpGet("course")]
    public async Task<IActionResult> GetAllCoursesAsync()
    {
        var result = await _adminService.GetAllCoursesAsync();
        return Ok(result);
    }

    #endregion

    #region ClassRoom

    [HttpPost("classroom")]
    public async Task<IActionResult> CreateClassroom([FromBody] CreateClassRoomDto dto)
    {
        await _adminService.CreateClassRoomAsync(dto);
        return Ok("Classroom created successfully");
    }
    [HttpPost("classroomP")]
    public async Task<IActionResult> UpdateClassroomAsync([FromBody] UpdateClassRoomDto dto)
    {
        await _adminService.UpdateClassRoomAsync(dto);
        return Ok("Classroom Updated successfully");
    }
    [HttpDelete("classroom/{ClassroomId}")]
    public async Task<IActionResult> SoftDeleteClassroomAsync([FromRoute] int classroomId)
    {
        await _adminService.SoftDeleteClassRoomAsync(classroomId);
        return Ok("Classroom SoftDeleted successfully");
    }

    [HttpDelete("classroom/{ClassroomId}/hard")]
    public async Task<IActionResult> HardDeleteClassroomAsync([FromRoute] int classroomId)
    {
        await _adminService.HardDeleteClassRoomAsync(classroomId);
        return Ok("Classroom HardDeleted successfully");
    }
    [HttpGet("classroom")]
    public async Task<IActionResult> GetAllClassroomsAsync()
    {
        var result = await _adminService.GetAllClassRoomsAsync();
        return Ok(result);
    }

    #endregion

    #region EnrollStudent

    [HttpPost("enrollstudent")]
    public async Task<IActionResult> EnrollStudentAsync([FromBody] EnrollStudentDto dto)
    {
        await _adminService.EnrollStudentAsync(dto);
        return Ok("Enroll Student successfully");
    }

    [HttpDelete("enrollstudent/{StudentId}")]
    public async Task<IActionResult> SoftDeleteEnrollStudentAsync([FromRoute] int studentId)
    {
        await _adminService.SoftDeleteStudentAsync(studentId);
        return Ok("Enroll Student SoftDeleted successfully");
    }

    [HttpDelete("enrollstudent/{StudentId}/hard")]
    public async Task<IActionResult> HardDeleteEnrollStudentAsync([FromRoute] int studentId)
    {
        await _adminService.HardDeleteStudentAsync(studentId);
        return Ok("Enroll Student HardDeleted successfully");
    }

    [HttpGet("enrollstudent")]
    public async Task<IActionResult> GetAllEnrollStudentsAsync()
    {
        var result= await _adminService.GetAllEnrollmentsAsync();
        return Ok(result);
    }
    #endregion

    #region Pagination
    [HttpPost("student_pagination")]
    public async Task<IActionResult> StudentPagination([FromBody]PaginationRequestDto dto)
    {
        var result= await _adminService.PaginationStudents(dto);
        return Ok(result);
    }
    [HttpPost("teacher_pagination")]
    public async Task<IActionResult> TeacherPagination([FromBody]  PaginationRequestDto dto)
    {
        var result = await _adminService.PaginationTeachers(dto);
        return Ok(result);
    }
    [HttpPost("admin_pagination")]
    public async Task<IActionResult> AdminPagination([FromBody]PaginationRequestDto dto)
    {
        var result= await _adminService.PaginationAdmins(dto);
        return Ok(result);
    }
    [HttpPost("classroom_pagination")]
    public async Task<IActionResult> ClassroomPagination([FromBody]PaginationRequestDto dto)
    {
        var result = await _adminService.PaginationClassRooms(dto);
        return Ok(result);
    }
    [HttpPost("class_session_pagination")]
    public async Task<IActionResult> PaginationClassSession([FromBody]PaginationRequestDto dto)
    {
        var result = await _adminService.PaginationClassSession(dto);
        return Ok(result);
    }
    [HttpPost("term _pagination")]
    public async Task<IActionResult> PaginationTerm([FromBody] PaginationRequestDto dto)
    {
        var result = await _adminService.PaginationTerm(dto);
        return Ok(result);
    }
    
    #endregion
}  

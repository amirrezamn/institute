using System.Security.Claims;
using institute.DTOs;
using institute.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace institute.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    [Authorize(Roles = "Teacher")] 
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // ================= Mark Attendance =================
        [HttpPost("mark-attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceDto dto)
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (teacherId == 0)
                return Unauthorized("Invalid teacher ID");

            await _teacherService.MarkAttendanceAsync(dto);
            return Ok(new { message = "Attendance marked successfully" });
        }

        // ================= Give Grade =================
        [HttpPost("give-grade")]
        public async Task<IActionResult> GiveGrade([FromBody] GiveGradeDto dto)
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (teacherId == 0)
                return Unauthorized("Invalid teacher ID");

            await _teacherService.GiveGradeAsync(dto, teacherId);
            return Ok(new { message = "Grade assigned successfully" });
        }
    }
}
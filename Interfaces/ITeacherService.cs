using institute.DTOs;

namespace institute.Interfaces;

public interface ITeacherService
{
    Task MarkAttendanceAsync(MarkAttendanceDto dto);
    Task GiveGradeAsync(GiveGradeDto dto, int teacherId);
}
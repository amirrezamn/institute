using institute.DTOs;
using institute.Entities;
using institute.Interfaces;

namespace institute.Services;

public class TeacherService:ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public TeacherService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
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

    public async Task GiveGradeAsync(GiveGradeDto dto, int teacherId)
    {
        var teacher= (await _unitOfWork.TeacherProfiles.FindAsync(t=>t.UserId == teacherId)).FirstOrDefault();
        if (teacher == null)
        {
            throw new Exception("Teacher not found");
        }
        var classroom=(await _unitOfWork.ClassRooms.FindAsync(c=>c.Id == dto.ClassRoomId &&  c.TeacherId == teacherId)).FirstOrDefault();
        if (classroom == null)
        {
            throw new Exception("Class room not found");
        }
        var enrollment= (await _unitOfWork.Enrollments.FindAsync(e=>e.StudentId==dto.StudentId&& e.ClassRoomId==dto.ClassRoomId)).FirstOrDefault();
        if (enrollment == null)
        {
            throw new Exception("Enrollment not found");
        }
        var existingGrade= (await _unitOfWork.Grades.FindAsync(g=>g.StudentId==dto.StudentId && g.ClassRoomId==dto.ClassRoomId)).FirstOrDefault();
        if (existingGrade != null)
        {
            await _unitOfWork.Grades.UpdateAsync(existingGrade);
        }
        var total =
            dto.ClassActivity +
            dto.Speaking +
            dto.Listening +
            dto.Writing +
            dto.Midterm +
            dto.FinalExam;

        var isPassed = total >= 70;

        await _unitOfWork.Grades.AddAsync(new Grade
        {
            StudentId = dto.StudentId,
            ClassRoomId = dto.ClassRoomId,
            FinalExam =  dto.FinalExam,
            Speaking =  dto.Speaking,
            Writing = dto.Writing,
            Listening =dto.Listening,
            Midterm = dto.Midterm,
            IsPassed = isPassed
        });
        await _unitOfWork.CompleteAsync();
    }

}
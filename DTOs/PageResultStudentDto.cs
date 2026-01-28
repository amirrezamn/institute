using System.ComponentModel.DataAnnotations;

namespace institute.DTOs;

public class PageResultStudentDto
{
 public int CurrentPage { get; set; }
 public int PageCount { get; set; }
 public List<StudentDto> users { get; set; }
}
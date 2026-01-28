namespace institute.DTOs;

public class PaginationRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }   // برای سرچ
    public string? SortBy { get; set; }    // اسم فیلد
    public bool Ascending { get; set; } = true;
}
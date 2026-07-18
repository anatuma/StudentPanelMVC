using StudentPanel.Shared.DTOs;

namespace StudentPanel.Client.Services;

public class StudentDetailsResponse
{
    public required StudentDto Student { get; set; }
    public List<CourseDto> Courses { get; set; } = new();
}

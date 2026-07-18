using System.ComponentModel.DataAnnotations;

namespace StudentPanel.Shared.DTOs;

public class StudentCourseDto
{
    [Required(ErrorMessage = "Student ID is required.")]
    public int StudentId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Select a course.")]
    public int CourseId { get; set; }
    
    public DateTime AssignedAt { get; set; } = DateTime.Now;
}

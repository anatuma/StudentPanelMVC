using System.ComponentModel.DataAnnotations;

namespace StudentPanel.Shared.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Course name is required.")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Course ects number is required.")]
    public int Ects { get; set; }
}
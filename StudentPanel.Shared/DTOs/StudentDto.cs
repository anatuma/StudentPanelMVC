using System.ComponentModel.DataAnnotations;

namespace StudentPanel.Shared.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Index number is required.")]
    public string IndexNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } =  string.Empty;
    
    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Semester is required.")]
    [Range(1, 8, ErrorMessage = "Semester must be between 1 and 8.")]
    public int Semester { get; set; } = 1;
}
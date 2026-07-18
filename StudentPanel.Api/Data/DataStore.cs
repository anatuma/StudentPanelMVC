using StudentPanel.Shared.DTOs;

namespace StudentPanel.Api.Data;

public class DataStore
{
    public static List<StudentDto> Students { get; set; } = new()
    {
        new StudentDto
        {
            Id = 1, IndexNumber = "s00067", Email = "s00067@pjwstk.edu.pl", FirstName = "John",
            LastName = "Pork", Semester = 4
        },
        new StudentDto
        {
            Id = 2, IndexNumber = "s00028", Email = "s00028@pjwstk.edu.pl", FirstName = "Ryan", LastName = "Gosling", Semester = 3
        }
    };

    public static List<CourseDto> Courses { get; set; } = new()
    {
        new CourseDto { Id = 1, Name = "APBD", Ects = 6 },
        new CourseDto { Id = 2, Name = "TPO", Ects = 6 },
        new CourseDto { Id = 3, Name = "PRI", Ects = 5 }
    };

    public static List<StudentCourseDto> StudentCourses { get; set; } = new()
    {
        new StudentCourseDto { StudentId = 1, CourseId = 1, AssignedAt = DateTime.Now.AddDays(-20) },
        new StudentCourseDto { StudentId = 1, CourseId = 2, AssignedAt = DateTime.Now.AddDays(-10) },
        new StudentCourseDto { StudentId = 2, CourseId = 2, AssignedAt = DateTime.Now.AddDays(-5) }
    };
}
using System.Net.Http.Json;
using StudentPanel.Shared;
using StudentPanel.Shared.DTOs;

namespace StudentPanel.Client.Services;

public class StudentsApiClient
{
    private readonly HttpClient _http;

    public StudentsApiClient(HttpClient http)
    {
        _http = http;
    }

    //GET /api/students
    public async Task<List<StudentDto>> GetStudentsAsync()
    {
        return await _http.GetFromJsonAsync<List<StudentDto>>("api/students") ?? new();
    }

    //GET /api/students/{id}
    public async Task<StudentDetailsResponse> GetStudentDetailsAsync(int id)
    {
        return await _http.GetFromJsonAsync<StudentDetailsResponse>($"api/students/{id}") 
               ?? throw new Exception("Failed to load student details.");
    }

    //POST /api/students
    public async Task<StudentDto> CreateStudentAsync(StudentDto newStudent)
    {
        var response = await _http.PostAsJsonAsync("api/students", newStudent);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<StudentDto>() 
               ?? throw new Exception("Failed to read created student.");
    }

    //GET /api/courses
    public async Task<List<CourseDto>> GetCoursesAsync()
    {
        return await _http.GetFromJsonAsync<List<CourseDto>>("api/courses") ?? new();
    }

    //POST /api/students/{id}/courses
    public async Task AssignCourseAsync(int studentId, int courseId)
    {
        var assignment = new StudentCourseDto { StudentId = studentId, CourseId = courseId };
        var response = await _http.PostAsJsonAsync($"api/students/{studentId}/courses", assignment);

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(string.IsNullOrWhiteSpace(message)
                ? "Unable to assign the selected course."
                : message);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using StudentPanel.Api.Data;
using StudentPanel.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors();

app.MapGet("/api/students", () => Results.Ok(DataStore.Students));
app.MapGet("/api/students/{id:int}", (int id) =>
{
   var student = DataStore.Students.FirstOrDefault(s => s.Id == id);
   if (student == null)
   {
       return Results.NotFound($"Student with ID {id} not found.");
   }
   var assignedCourses = DataStore.Courses
       .Where(c => DataStore.StudentCourses.Any(sc => sc.StudentId == id && sc.CourseId == c.Id))
       .ToList();
   return Results.Ok(new {Student = student, Courses = assignedCourses});
});

app.MapPost("/api/students", ([FromBody] StudentDto newStudent) =>
{
    newStudent.Id = DataStore.Students.Any() ? DataStore.Students.Max(s => s.Id)+1 : 1;
    DataStore.Students.Add(newStudent);
    return Results.Created($"/api/students/{newStudent.Id}", newStudent);
});

app.MapGet("/api/courses", () => Results.Ok(DataStore.Courses));

app.MapPost("/api/students/{id:int}/courses", (int id, [FromBody] StudentCourseDto assignment) =>
{
    var studentExists =  DataStore.Students.Any(s => s.Id == id);
    if (!studentExists)
    {
        return Results.NotFound($"Student with ID {id} not found.");
    }

    var courseExists = DataStore.Courses.Any(c => c.Id == assignment.CourseId);
    if (!courseExists)
    {
        return Results.BadRequest($"Course with ID {assignment.CourseId} does not exist.");
    }

    var alreadyAssigned = DataStore.StudentCourses.Any(sc => sc.StudentId == id && sc.CourseId == assignment.CourseId);
    if (alreadyAssigned)
    {
        return Results.BadRequest($"Student with ID {id} has already been assigned to Course with ID {assignment.CourseId}.");
    }

    assignment.StudentId = id;
    assignment.AssignedAt = DateTime.Now;
    DataStore.StudentCourses.Add(assignment);
    return Results.Ok($"Student with ID {id} has been successfully assigned to Course with ID {assignment.CourseId}.");
});

app.Run();

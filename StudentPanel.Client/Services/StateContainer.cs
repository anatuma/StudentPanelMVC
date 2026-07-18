using StudentPanel.Shared.DTOs;

namespace StudentPanel.Client.Services;

public class StateContainer
{
    private readonly List<StudentDto> _observedStudents = new();
    
    public IReadOnlyList<StudentDto> ObservedStudents => _observedStudents.AsReadOnly();

    public event Action? OnChange;

    public void AddObservedStudent(StudentDto student)
    {
        if (!_observedStudents.Any(s => s.Id == student.Id))
        {
            _observedStudents.Add(student);
            NotifyStateChanged();
        }
    }

    public void RemoveObservedStudent(int studentId)
    {
        var studentToRemove = _observedStudents.FirstOrDefault(s => s.Id == studentId);
        if (studentToRemove != null)
        {
            _observedStudents.Remove(studentToRemove);
            NotifyStateChanged();
        }
    }

    public bool IsObserved(int studentId)
    {
        return _observedStudents.Any(s => s.Id == studentId);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
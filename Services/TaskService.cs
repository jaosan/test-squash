using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskItem Create(string title, string description, TaskCategory category = TaskCategory.General)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty.", nameof(title));

        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title.Trim(),
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            Category = category
        };
        _tasks.Add(task);
        return task;
    }

    public IReadOnlyList<TaskItem> GetAll()
    {
        return _tasks.AsReadOnly();
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public bool Complete(int id)
    {
        var task = GetById(id);
        if (task is null) return false;

        task.IsCompleted = true;
        return true;
    }

    public bool Delete(int id)
    {
        var task = GetById(id);
        if (task is null) return false;

        _tasks.Remove(task);
        return true;
    }

    public bool Assign(int taskId, int userId)
    {
        var task = GetById(taskId);
        if (task is null) return false;

        task.AssignedUserId = userId;
        return true;
    }

    public IReadOnlyList<TaskItem> FilterByStatus(bool isCompleted)
    {
        return _tasks.Where(t => t.IsCompleted == isCompleted).ToList().AsReadOnly();
    }

    public IReadOnlyList<TaskItem> GetByCategory(TaskCategory category)
    {
        return _tasks.Where(t => t.Category == category).ToList().AsReadOnly();
    }
}

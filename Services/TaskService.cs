using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    private static void ValidateTaskInput(string title, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty.", nameof(title));

        if (title.Length > 250)
            throw new ArgumentException("Task title must not exceed 250 characters.", nameof(title));

        if (description is not null && description.Length > 5000)
            throw new ArgumentException("Task description must not exceed 5000 characters.", nameof(description));
    }

    public TaskItem Create(string title, string description, TaskCategory category = TaskCategory.General, DateTime? dueDate = null)
    {
        ValidateTaskInput(title, description);

        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title.Trim(),
            Description = description ?? string.Empty,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            Category = category,
            DueDate = dueDate
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

    public IReadOnlyList<TaskItem> GetOverdue()
    {
        return _tasks.Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value < DateTime.UtcNow)
                     .ToList().AsReadOnly();
    }

    public IReadOnlyList<TaskItem> Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return GetAll();

        var lower = keyword.ToLowerInvariant();
        return _tasks.Where(t =>
            t.Title.ToLowerInvariant().Contains(lower) ||
            t.Description.ToLowerInvariant().Contains(lower))
            .ToList().AsReadOnly();
    }
}

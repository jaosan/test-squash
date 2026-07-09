using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskItem Create(string title, string description)
    {
        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
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
}

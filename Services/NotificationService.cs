using TaskManager.Models;

namespace TaskManager.Services;

public class NotificationService
{
    private readonly List<string> _notifications = new();

    public void Notify(string message)
    {
        _notifications.Add($"[{DateTime.UtcNow:HH:mm:ss}] {message}");
        Console.WriteLine($"  🔔 {message}");
    }

    public void NotifyTaskCreated(TaskItem task)
    {
        Notify($"Task created: {task.Title}");
    }

    public void NotifyTaskCompleted(TaskItem task)
    {
        Notify($"Task completed: {task.Title}");
    }

    public void NotifyTaskOverdue(TaskItem task)
    {
        Notify($"OVERDUE: {task.Title} was due on {task.DueDate:yyyy-MM-dd}");
    }

    public IReadOnlyList<string> GetAll()
    {
        return _notifications.AsReadOnly();
    }
}

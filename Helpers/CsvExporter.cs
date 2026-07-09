using TaskManager.Models;

namespace TaskManager.Helpers;

public static class CsvExporter
{
    public static string ExportTasks(IReadOnlyList<TaskItem> tasks)
    {
        var lines = new List<string> { "Id,Title,Description,IsCompleted,Category,CreatedAt" };
        foreach (var task in tasks)
        {
            lines.Add($"{task.Id},{Escape(task.Title)},{Escape(task.Description)},{task.IsCompleted},{task.Category},{task.CreatedAt:yyyy-MM-dd}");
        }
        return string.Join(Environment.NewLine, lines);
    }

    private static string Escape(string value)
    {
        if (value.Contains(',') || value.Contains('"'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }
}

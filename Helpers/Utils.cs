namespace TaskManager.Helpers;

public static class Utils
{
    public static string FormatDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm");
    }

    public static string FormatDueDate(DateTime? dueDate)
    {
        if (!dueDate.HasValue) return string.Empty;
        var isOverdue = dueDate.Value < DateTime.UtcNow;
        return isOverdue ? $"[OVERDUE: {dueDate.Value:yyyy-MM-dd}]" : $"[due: {dueDate.Value:yyyy-MM-dd}]";
    }

    public static void PrintSeparator()
    {
        Console.WriteLine(new string('-', 50));
    }

    public static int? ReadInt(string prompt)
    {
        Console.Write(prompt);
        return int.TryParse(Console.ReadLine(), out var value) ? value : null;
    }

    public static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }
}

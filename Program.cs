using TaskManager.Helpers;
using TaskManager.Models;
using TaskManager.Services;

var taskService = new TaskService();
var userService = new UserService();

while (true)
{
    Console.WriteLine();
    Utils.PrintSeparator();
    Console.WriteLine("  TaskManager");
    Utils.PrintSeparator();
    Console.WriteLine("  1. List tasks");
    Console.WriteLine("  2. Create task");
    Console.WriteLine("  3. Complete task");
    Console.WriteLine("  4. Delete task");
    Console.WriteLine("  5. Filter tasks by status");
    Console.WriteLine("  6. Filter tasks by category");
    Console.WriteLine("  7. List users");
    Console.WriteLine("  8. Create user");
    Console.WriteLine("  0. Exit");
    Utils.PrintSeparator();

    var choice = Utils.ReadInt("Select option: ");

    switch (choice)
    {
        case 1:
            var tasks = taskService.GetAll();
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks found.");
                break;
            }
            foreach (var t in tasks)
            {
                var status = t.IsCompleted ? "[x]" : "[ ]";
                Console.WriteLine($"  {status} [{t.Id}] {t.Title} [{t.Category}] - {Utils.FormatDate(t.CreatedAt)}");
            }
            break;

        case 2:
            var title = Utils.ReadString("Title: ");
            var desc = Utils.ReadString("Description: ");
            Console.WriteLine("Categories: General, Work, Personal, Urgent");
            var catInput = Utils.ReadString("Category (default: General): ");
            var category = Enum.TryParse<TaskCategory>(catInput, true, out var parsedCat) ? parsedCat : TaskCategory.General;
            var created = taskService.Create(title, desc, category);
            Console.WriteLine($"Task #{created.Id} created.");
            break;

        case 3:
            var completeId = Utils.ReadInt("Task ID: ");
            if (completeId.HasValue && taskService.Complete(completeId.Value))
                Console.WriteLine("Task marked as completed.");
            else
                Console.WriteLine("Task not found.");
            break;

        case 4:
            var deleteId = Utils.ReadInt("Task ID: ");
            if (deleteId.HasValue && taskService.Delete(deleteId.Value))
                Console.WriteLine("Task deleted.");
            else
                Console.WriteLine("Task not found.");
            break;

        case 5:
            Console.Write("Show completed? (y/n): ");
            var showCompleted = Console.ReadLine()?.Trim().ToLower() == "y";
            var filtered = taskService.FilterByStatus(showCompleted);
            foreach (var t in filtered)
            {
                var status = t.IsCompleted ? "[x]" : "[ ]";
                Console.WriteLine($"  {status} [{t.Id}] {t.Title}");
            }
            break;

        case 6:
            Console.WriteLine("Categories: General, Work, Personal, Urgent");
            var catFilter = Utils.ReadString("Category: ");
            if (Enum.TryParse<TaskCategory>(catFilter, true, out var filterCat))
            {
                var catTasks = taskService.GetByCategory(filterCat);
                foreach (var t in catTasks)
                    Console.WriteLine($"  [{t.Id}] {t.Title} - {Utils.FormatDate(t.CreatedAt)}");
            }
            else
            {
                Console.WriteLine("Invalid category.");
            }
            break;

        case 7:
            var users = userService.GetAll();
            if (!users.Any())
            {
                Console.WriteLine("No users found.");
                break;
            }
            foreach (var u in users)
                Console.WriteLine($"  [{u.Id}] {u.Name} <{u.Email}>");
            break;

        case 8:
            var name = Utils.ReadString("Name: ");
            var email = Utils.ReadString("Email: ");
            var newUser = userService.Create(name, email);
            Console.WriteLine($"User #{newUser.Id} created.");
            break;

        case 0:
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

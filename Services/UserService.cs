using TaskManager.Models;

namespace TaskManager.Services;

public class UserService
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public User Create(string name, string email)
    {
        var user = new User
        {
            Id = _nextId++,
            Name = name,
            Email = email,
            CreatedAt = DateTime.UtcNow
        };
        _users.Add(user);
        return user;
    }

    public IReadOnlyList<User> GetAll()
    {
        return _users.AsReadOnly();
    }

    public IReadOnlyList<User> GetAllSortedByName()
    {
        return _users.OrderBy(u => u.Name).ToList().AsReadOnly();
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User? FindByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public bool Delete(int id)
    {
        var user = GetById(id);
        if (user is null) return false;

        _users.Remove(user);
        return true;
    }
}

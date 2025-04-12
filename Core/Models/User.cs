namespace Core.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;

    public ICollection<Habit> Habits { get; set; } = new List<Habit>();
}

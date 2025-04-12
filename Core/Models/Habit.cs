namespace Core.Models;

public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Frequency { get; set; } = "daily";

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<HabitCheckin> Checkins { get; set; } = new List<HabitCheckin>();
}

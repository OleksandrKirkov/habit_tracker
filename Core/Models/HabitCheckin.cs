namespace Core.Models;

public class HabitCheckin
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool Completed { get; set; }

    public int HabitId { get; set; }
    public Habit Habit { get; set; } = null!;
}

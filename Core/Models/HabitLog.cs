using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class HabitLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int HabitId { get; set; }

        [ForeignKey(nameof(HabitId))]
        public Habit Habit { get; set; } = null!;

        [Required]
        [Column(TypeName = "date")]
        public DateTime LogDate { get; set; }

        public int? Value { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}

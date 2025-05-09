using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class HabitLog
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid HabitId { get; set; }

        [ForeignKey(nameof(HabitId))]
        public Habits Habits { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime LogDate { get; set; }

        public int? Value { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}

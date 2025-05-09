using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class SyncBackup
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Column(TypeName = "bytea")]
        public byte[] DataBlob { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

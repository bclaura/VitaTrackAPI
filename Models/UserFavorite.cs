using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VitaTrackAPI.Models
{
    [Table("user_favorites")]
    public class UserFavorite
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        [Column("doctor_id")]
        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}

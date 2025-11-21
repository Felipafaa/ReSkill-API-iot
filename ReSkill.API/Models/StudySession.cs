using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReSkill.API.Models
{
    [Table("tb_study_sessions")]
    public class StudySession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; } 

        [Required]
        public int DurationMinutes { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsCompleted { get; set; } = true;
    }
}
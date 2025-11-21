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
        public string Topic { get; set; } // Ex: "C# Básico"

        [Required]
        public int DurationMinutes { get; set; } // Tempo vindo do dispositivo

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Para simular a lógica de consistência futura
        public bool IsCompleted { get; set; } = true;
    }
}
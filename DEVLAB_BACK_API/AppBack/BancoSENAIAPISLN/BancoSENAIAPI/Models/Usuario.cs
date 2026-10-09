using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string SenhaHash { get; set; }
    }
}

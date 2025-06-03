using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NewsMVC.Models
{
    public class New
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zadej Název.")]
        [StringLength(50, ErrorMessage = "Maximálně 50 znaků.")]
        public string? Title { get; set; }
        public DateTime DateT { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Zadej text příspěvku.")]
        [StringLength(500, ErrorMessage = "Maximálně 500 znaků.")]
        public string? Text { get; set; }
        public bool IsOnTop { get; set; }

        public string? AutorID { get; set; }
        [Required]
        public IdentityUser? Autor { get; set; }
    }
}

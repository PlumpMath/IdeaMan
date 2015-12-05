using System.ComponentModel.DataAnnotations;

namespace IdeaManMVC.Models.Ideas
{
    public class Comment
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }
        [Required]
        public IdeaEntry Idea { get; set; }
        [Required]
        public ApplicationUser Author { get; set; }
    }
}
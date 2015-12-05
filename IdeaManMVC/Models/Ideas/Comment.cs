using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaManMVC.Models.Ideas
{
    public class Comment
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment text")]
        public string Text { get; set; }

        [Required]
        public int Idea_Id { get; set; }
        
        [ForeignKey("Idea_Id")]
        public IdeaEntry Idea { get; set; }

        [Required]
        public string Author_Id { get; set; }

        [ForeignKey("Author_Id")]
        public virtual ApplicationUser Author { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdeaManMVC.Models.Ideas;
using Microsoft.AspNet.Identity;

namespace IdeaManMVC.Models
{
    public class IdeaEntry : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(180)]
        public string ShortDescription { get; set; }
        [Required]
        public string FullText { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public List<Vote> Votes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
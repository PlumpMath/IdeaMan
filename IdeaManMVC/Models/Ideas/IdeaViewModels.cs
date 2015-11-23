using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdeaManMVC.Models.Ideas
{
    public class IdeaShortViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum length for {0} is {2}")]
        [MaxLength(30, ErrorMessage = "Maximum length for {0} is {2}")]
        public string Title { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Minimum length for {0} is {2}")]
        [MaxLength(180, ErrorMessage = "Maximum length for {0} is {2}")]
        public string ShortDescription { get; set; }

        [MinLength(100, ErrorMessage = "Minimum length for {0} is {2}")]
        public string FullText { get; set; }

        public string CreatorName { get; set; }
    }
}
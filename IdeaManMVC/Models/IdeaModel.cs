using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaManMVC.Models
{
    public class IdeaModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullText { get; set; }
        public ApplicationUser Creator { get; set; }
        public ApplicationUser Owner { get; set; }
        
    }
}
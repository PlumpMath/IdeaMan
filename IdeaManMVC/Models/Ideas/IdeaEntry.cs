using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace IdeaManMVC.Models
{
    public class IdeaEntry : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullText { get; set; }
        public virtual ApplicationUser Creator { get; set; }
    }
}
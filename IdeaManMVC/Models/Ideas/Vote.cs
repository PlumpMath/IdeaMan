using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaManMVC.Models.Ideas
{
    public class Vote : BaseEntity
    {
        public int Id { get; set; }
        public virtual IdeaEntry Idea { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
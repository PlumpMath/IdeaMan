using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdeaManMVC.Models;
using IdeaManMVC.Models.Ideas;
using Microsoft.AspNet.Identity;

namespace IdeaManMVC.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Comments/Create
        public ActionResult Create(int ideaId = 0)
        {
            if(ideaId < 1) return RedirectToAction("Index", "IdeaEntry");
            var idea = db.Ideas.Find(ideaId);
            if(idea == null) return RedirectToAction("Index", "IdeaEntry");
            var comment = new Comment()
            {
                Idea_Id = ideaId,
                Idea = db.Ideas.Find(ideaId)
            };
            return View(comment);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Comment comment)
        {
            comment.Author_Id = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(comment);
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "IdeaEntry", new { @id=comment.Idea_Id });
            }
            return View(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

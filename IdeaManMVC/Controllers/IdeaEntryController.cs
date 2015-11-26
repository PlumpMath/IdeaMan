using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using IdeaManMVC.Models;
using IdeaManMVC.Models.Ideas;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdeaManMVC.Controllers
{
    [Authorize]
    public class IdeaEntryController : Controller
    {
        private ApplicationDbContext appDb { get; set; }
        private UserManager<ApplicationUser> userManager { get; set; }

        public IdeaEntryController():base()
        {
            appDb = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
        }
        // GET: IdeaEntry
        public async Task<ActionResult> Index()
        {
            var results = appDb.Ideas.OrderByDescending(idea => idea.DateCreated)
                .Include(o => o.Votes);
            
            return View(await results.ToListAsync());
        }

        // GET: IdeaEntry/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ideaEntry = appDb.Ideas.Where(idea=>idea.Id == id)?.FirstOrDefaultAsync();
            return View(await ideaEntry);
        }

        // GET: IdeaEntry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdeaEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,ShortDescription,FullText")] IdeaEntry ideaEntry)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindById(User.Identity.GetUserId());
                if (user == null)
                {
                    return View(ideaEntry);
                }
                ideaEntry.Creator = user;
                appDb.Ideas.Add(ideaEntry);
                await appDb.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            

            return View(ideaEntry);
        }

        // GET: IdeaEntry/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaEntry ideaEntry =  appDb.Ideas.FirstOrDefault(o => o.Id == id);
            if (ideaEntry == null)
            {
                return HttpNotFound();
            }
            if (ideaEntry.Creator.Id != User.Identity.GetUserId())
            {
                ViewBag.Error = "Permission denied. This idea does not belong to you";
                return RedirectToAction("Details", new { @id = id });
            }
            return View(ideaEntry);
        }

        // POST: IdeaEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,ShortDescription,FullText")] IdeaEntry ideaEntry)
        {
            if (ModelState.IsValid)
            {
                appDb.Entry(ideaEntry).State = EntityState.Modified;
                await appDb.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ideaEntry);
        }

        // GET: IdeaEntry/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaEntry ideaEntry = await appDb.Ideas.FindAsync(id);
            if (ideaEntry == null)
            {
                return HttpNotFound();
            }
            if (ideaEntry.Creator.Id != User.Identity.GetUserId())
            {
                ViewBag.Error = "Permission denied. This idea does not belong to you";
                return RedirectToAction("Details", new { @id = id });
            }
            return View(ideaEntry);
        }

        // POST: IdeaEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IdeaEntry ideaEntry = appDb.Ideas.Find(id);
            appDb.Votes.RemoveRange(appDb.Votes.Where(o => o.Idea.Id == ideaEntry.Id));
            appDb.Ideas.Remove(ideaEntry);
            await appDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //IdeaEntry/DoVote
        [Authorize]
        [HttpPost]
        public ActionResult DoVote(int id)
        {
            var userId = User.Identity.GetUserId();
            var hasVote = appDb.Votes
                .Where(o=>o.Idea.Id == id)
                .Any(o => o.User.Id == userId);
            if(hasVote)
            {
                this.Response.StatusCode = 403;
                return Json(new { result = "error", message="You have already voted for this."}); 
            }
            appDb.Votes.Add(new Vote()
            {
                    User = appDb.AppUsers.Find(userId),
                    Idea = appDb.Ideas.Find(id)
            });
            
            appDb.SaveChanges();
            return Json(new { result = "OK", message="Vote casted"} );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                appDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

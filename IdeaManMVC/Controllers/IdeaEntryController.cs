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
        // GET: IdeaModels
        public async Task<ActionResult> Index()
        {
            var results = appDb.Ideas.OrderByDescending(idea => idea.DateCreated);
            return View(await results.ToListAsync());
        }

        // GET: IdeaModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ideaEntry = appDb.Ideas.Where(idea=>idea.Id == id)?.FirstOrDefaultAsync();
            return View(await ideaEntry);
        }

        // GET: IdeaModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdeaModels/Create
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

        // GET: IdeaModels/Edit/5
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

        // POST: IdeaModels/Edit/5
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

        // GET: IdeaModels/Delete/5
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
            return View(ideaEntry);
        }

        // POST: IdeaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IdeaEntry ideaEntry = await appDb.Ideas.FindAsync(id);
            appDb.Ideas.Remove(ideaEntry);
            await appDb.SaveChangesAsync();
            return RedirectToAction("Index");
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

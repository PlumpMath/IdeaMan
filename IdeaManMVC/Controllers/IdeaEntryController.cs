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

namespace IdeaManMVC.Controllers
{
    [Authorize]
    public class IdeaEntryController : Controller
    {
        private IdeasDbContext db = new IdeasDbContext();

        // GET: IdeaModels
        public async Task<ActionResult> Index()
        {
            return View(await db.Ideas.ToListAsync());
        }

        // GET: IdeaModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaEntry ideaEntry = await db.Ideas.FindAsync(id);
            if (ideaEntry == null)
            {
                return HttpNotFound();
            }
            return View(ideaEntry);
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
                db.Ideas.Add(ideaEntry);
                await db.SaveChangesAsync();
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
            IdeaEntry ideaEntry = await db.Ideas.FindAsync(id);
            if (ideaEntry == null)
            {
                return HttpNotFound();
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
                db.Entry(ideaEntry).State = EntityState.Modified;
                await db.SaveChangesAsync();
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
            IdeaEntry ideaEntry = await db.Ideas.FindAsync(id);
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
            IdeaEntry ideaEntry = await db.Ideas.FindAsync(id);
            db.Ideas.Remove(ideaEntry);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

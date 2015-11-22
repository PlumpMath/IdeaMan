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

namespace IdeaManMVC.Controllers
{
    [Authorize]
    public class IdeaModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IdeaModels
        public async Task<ActionResult> Index()
        {
            return View(await db.IdeaModels.ToListAsync());
        }

        // GET: IdeaModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaModel ideaModel = await db.IdeaModels.FindAsync(id);
            if (ideaModel == null)
            {
                return HttpNotFound();
            }
            return View(ideaModel);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,ShortDescription,FullText")] IdeaModel ideaModel)
        {
            if (ModelState.IsValid)
            {
                db.IdeaModels.Add(ideaModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ideaModel);
        }

        // GET: IdeaModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaModel ideaModel = await db.IdeaModels.FindAsync(id);
            if (ideaModel == null)
            {
                return HttpNotFound();
            }
            return View(ideaModel);
        }

        // POST: IdeaModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,ShortDescription,FullText")] IdeaModel ideaModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ideaModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ideaModel);
        }

        // GET: IdeaModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdeaModel ideaModel = await db.IdeaModels.FindAsync(id);
            if (ideaModel == null)
            {
                return HttpNotFound();
            }
            return View(ideaModel);
        }

        // POST: IdeaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IdeaModel ideaModel = await db.IdeaModels.FindAsync(id);
            db.IdeaModels.Remove(ideaModel);
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

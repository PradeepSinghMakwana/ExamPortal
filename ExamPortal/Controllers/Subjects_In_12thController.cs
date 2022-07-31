using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamPortal.Models;

namespace ExamPortal.Controllers
{
    public class Subjects_In_12thController : Controller
    {
        private ExamPortalEntities db = new ExamPortalEntities();

        // GET: Subjects_In_12th
        public async Task<ActionResult> Index()
        {
            return View(await db.Subjects_In_12th.ToListAsync());
        }

        // GET: Subjects_In_12th/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects_In_12th subjects_In_12th = await db.Subjects_In_12th.FindAsync(id);
            if (subjects_In_12th == null)
            {
                return HttpNotFound();
            }
            return View(subjects_In_12th);
        }

        // GET: Subjects_In_12th/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects_In_12th/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "subject_code,subject_name")] Subjects_In_12th subjects_In_12th)
        {
            if (ModelState.IsValid)
            {
                db.Subjects_In_12th.Add(subjects_In_12th);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subjects_In_12th);
        }

        // GET: Subjects_In_12th/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects_In_12th subjects_In_12th = await db.Subjects_In_12th.FindAsync(id);
            if (subjects_In_12th == null)
            {
                return HttpNotFound();
            }
            return View(subjects_In_12th);
        }

        // POST: Subjects_In_12th/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "subject_code,subject_name")] Subjects_In_12th subjects_In_12th)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjects_In_12th).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subjects_In_12th);
        }

        // GET: Subjects_In_12th/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects_In_12th subjects_In_12th = await db.Subjects_In_12th.FindAsync(id);
            if (subjects_In_12th == null)
            {
                return HttpNotFound();
            }
            return View(subjects_In_12th);
        }

        // POST: Subjects_In_12th/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Subjects_In_12th subjects_In_12th = await db.Subjects_In_12th.FindAsync(id);
            db.Subjects_In_12th.Remove(subjects_In_12th);
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

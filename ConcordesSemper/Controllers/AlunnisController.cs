using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConcordesSemper.Models;

namespace ConcordesSemper.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AlunnisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Alunnis
        public ActionResult Index()
        {
            var alunnis = db.Alunnis.Include(a => a.Casa);
            return View(alunnis.ToList());
        }

        // GET: Alunnis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunni alunni = db.Alunnis.Find(id);
            if (alunni == null)
            {
                return HttpNotFound();
            }
            return View(alunni);
        }

        // GET: Alunnis/Create
        public ActionResult Create()
        {
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome");
            return View();
        }

        // POST: Alunnis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Alunno_Id,Nome,Cognome,Casa_Id")] Alunni alunni)
        {
            if (ModelState.IsValid)
            {
                db.Alunnis.Add(alunni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", alunni.Casa_Id);
            return View(alunni);
        }

        // GET: Alunnis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunni alunni = db.Alunnis.Find(id);
            if (alunni == null)
            {
                return HttpNotFound();
            }
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", alunni.Casa_Id);
            return View(alunni);
        }

        // POST: Alunnis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Alunno_Id,Nome,Cognome,Casa_Id")] Alunni alunni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", alunni.Casa_Id);
            return View(alunni);
        }

        // GET: Alunnis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunni alunni = db.Alunnis.Find(id);
            if (alunni == null)
            {
                return HttpNotFound();
            }
            return View(alunni);
        }

        // POST: Alunnis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alunni alunni = db.Alunnis.Find(id);
            db.Alunnis.Remove(alunni);
            db.SaveChanges();
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

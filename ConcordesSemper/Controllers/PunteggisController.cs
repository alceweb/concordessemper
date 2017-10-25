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
    public class PunteggisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Punteggis
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var punteggis = db.Punteggis.Include(p => p.Nome).OrderByDescending(p=>p.Data);
            return View(punteggis.ToList());
        }

        // GET: Punteggis/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punteggi punteggi = db.Punteggis.Find(id);
            if (punteggi == null)
            {
                return HttpNotFound();
            }
            return View(punteggi);
        }

        // GET: Punteggis/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome");
            return View();
        }

        // POST: Punteggis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Punteggio_Id,Casa_Id,Data,Punti,Motivazione,Comportamento,Dimenticanze,Varie,GrandiG,OeP,Insegnante")] Punteggi punteggi)
        {
            if (ModelState.IsValid)
            {
                db.Punteggis.Add(punteggi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", punteggi.Casa_Id);
            return View(punteggi);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Assegna()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assegna(int casa, [Bind(Include = "Punteggio_Id,Casa_Id,Data,Punti,Motivazione,Comportamento,Dimenticanze,Varie,GrandiG,OeP,Insegnante")] Punteggi punteggi)
        {
            var tot = punteggi.Comportamento + punteggi.Dimenticanze + punteggi.GrandiG + punteggi.OeP + punteggi.Varie;

            if (ModelState.IsValid && tot != 0)
            {
                punteggi.Insegnante = User.Identity.Name;
                punteggi.Data = DateTime.Now;
                punteggi.Casa_Id = casa;
                db.Punteggis.Add(punteggi);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Non è stato assegnato nessun punteggio!!!";
            return View(punteggi);
        }

        [Authorize(Roles = "Admin,Insegnante")]
        public ActionResult AssegnaI()
        {
            return View();
        }

        [Authorize(Roles = "Insegnante,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssegnaI(int casa, [Bind(Include = "Punteggio_Id,Casa_Id,Data,Punti,Motivazione,Comportamento,Dimenticanze,Varie,GrandiG,OeP,Insegnante")] Punteggi punteggi)
        {
            var tot = punteggi.Comportamento + punteggi.Dimenticanze + punteggi.GrandiG + punteggi.OeP + punteggi.Varie;

            if (ModelState.IsValid && tot != 0)
            {
                punteggi.Insegnante = User.Identity.Name;
                punteggi.Data = DateTime.Now;
                punteggi.Casa_Id = casa;
                db.Punteggis.Add(punteggi);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Non è stato assegnato nessun punteggio!!!";
            return View(punteggi);
        }

        public ActionResult AssegnaTornei(int casa, int punti)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AssegnaTornei(int casa, int punti, int torneoId, DateTime datat, string insegnante, [Bind(Include = "Punteggio_Id,Casa_Id,Data,Varie,Motivazione,Insegnante")] Punteggi assegna)
        {
            if (ModelState.IsValid)
            {
                assegna.Data = DateTime.Now;
                assegna.Casa_Id = casa;
                assegna.Varie = punti;
                assegna.Motivazione = punti + " punti per risoluzione gara N." + torneoId + " dell'insegnante " + insegnante;  
                db.Punteggis.Add(assegna);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            return View(assegna);
        }

        // GET: Punteggis/Edit/5
        [Authorize(Roles = "Admin,Insegnante")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punteggi punteggi = db.Punteggis.Find(id);
            if (punteggi == null)
            {
                return HttpNotFound();
            }
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", punteggi.Casa_Id);
            return View(punteggi);
        }

        // POST: Punteggis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Insegnante")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Punteggio_Id,Casa_Id,Data,Punti,Motivazione,Comportamento,Dimenticanze,Varie,GrandiG,OeP,Insegnante")] Punteggi punteggi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(punteggi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", punteggi.Casa_Id);
            return View(punteggi);
        }

        // GET: Punteggis/Edit/5
        [Authorize(Roles = "Admin,Insegnante")]
        public ActionResult EditIns(int? id, int? casa)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punteggi punteggi = db.Punteggis.Find(id);
            if (punteggi == null)
            {
                return HttpNotFound();
            }
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", punteggi.Casa_Id);
            return View(punteggi);
        }

        [Authorize(Roles = "Admin,Insegnante")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIns(int? casa, [Bind(Include = "Punteggio_Id,Casa_Id,Data,Punti,Motivazione,Comportamento,Dimenticanze,Varie,GrandiG,OeP,Insegnante")] Punteggi punteggi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(punteggi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Content", "Cases", new {id = casa });
            }
            ViewBag.Casa_Id = new SelectList(db.Cases, "Casa_Id", "Nome", punteggi.Casa_Id);
            return View(punteggi);
        }

        // GET: Punteggis/Delete/5
        [Authorize(Roles = "Admin,Insegnante")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punteggi punteggi = db.Punteggis.Find(id);
            if (punteggi == null)
            {
                return HttpNotFound();
            }
            return View(punteggi);
        }

        // POST: Punteggis/Delete/5
        [Authorize(Roles = "Admin,Insegnante")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Punteggi punteggi = db.Punteggis.Find(id);
            db.Punteggis.Remove(punteggi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Insegnante")]
        public ActionResult DeleteIns(int? id, int? casa)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Punteggi punteggi = db.Punteggis.Find(id);
            if (punteggi == null)
            {
                return HttpNotFound();
            }
            return View(punteggi);
        }

        [Authorize(Roles = "Admin,Insegnante")]
        [HttpPost, ActionName("DeleteIns")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInsConfirmed(int id, int casa)
        {
            Punteggi punteggi = db.Punteggis.Find(id);
            db.Punteggis.Remove(punteggi);
            db.SaveChanges();
            return RedirectToAction("Content", "Cases", new {id = casa });
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Prof(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var punteggis = db.Punteggis.Include(p => p.Nome).Where(p=>p.Insegnante == username).OrderByDescending(p => p.Data);
            return View(punteggis.ToList());

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

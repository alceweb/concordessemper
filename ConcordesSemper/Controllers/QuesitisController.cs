using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConcordesSemper.Models;
using Microsoft.AspNet.Identity;

namespace ConcordesSemper.Controllers
{
    public class QuesitisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quesitis
        [Authorize]
        public ActionResult Index()
        {
            var prof = User.Identity.GetUserName();
            if (User.IsInRole("Admin"))
            {
            var quesiti = db.Quesitis.OrderByDescending(q=>q.DataI).ToList();
            ViewBag.QuesitiCount = quesiti.Count();
            return View(quesiti);

            }
            else
            {
                var quesiti = db.Quesitis.Where(q => q.Prof == prof).OrderByDescending(q=>q.DataI).ToList();
                ViewBag.QuesitiCount = quesiti.Count();
                return View(quesiti);
            }

        }
        public ActionResult IndexUt()
        {
            var quesiti = db.Quesitis.Where(q => q.Pubblica == true && q.DataI == q.DataF).OrderByDescending(q => q.DataI);
            return View(quesiti);
        }

        // GET: Quesitis/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesiti quesiti = db.Quesitis.Find(id);
            if (quesiti == null)
            {
                return HttpNotFound();
            }
            return View(quesiti);
        }

        // GET: Quesitis/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quesitis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Quesito_Id,Prof,Valore,Domanda_1,Risposta_1,Domanda_2,Risposta_2,Domanda_3,Risposta_3,Domanda_4,Risposta_4,Domanda_5,Risposta_5,ParolaChiave,Pubblica,DataI,DataF,Casa_Id,Cognome,Nome")] Quesiti quesiti)
        {
            if (ModelState.IsValid)
            {
                quesiti.Prof = User.Identity.GetUserName();
                quesiti.DataI = DateTime.Now;
                quesiti.DataF = quesiti.DataI;
                db.Quesitis.Add(quesiti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quesiti);
        }

        // GET: Quesitis/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesiti quesiti = db.Quesitis.Find(id);
            if (quesiti == null)
            {
                return HttpNotFound();
            }
            return View(quesiti);
        }

        // POST: Quesitis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Quesito_Id,Prof,Valore,Domanda_1,Risposta_1,Domanda_2,Risposta_2,Domanda_3,Risposta_3,Domanda_4,Risposta_4,Domanda_5,Risposta_5,ParolaChiave,Pubblica,DataI,DataF,Casa_Id,Cognome,Nome")] Quesiti quesiti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quesiti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quesiti);
        }

        // GET: Quesitis/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesiti quesiti = db.Quesitis.Find(id);
            if (quesiti == null)
            {
                return HttpNotFound();
            }
            return View(quesiti);
        }

        // POST: Quesitis/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quesiti quesiti = db.Quesitis.Find(id);
            db.Quesitis.Remove(quesiti);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Risposta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesiti quesiti = db.Quesitis.Find(id);
            if (quesiti == null)
            {
                return HttpNotFound();
            }
            return View(quesiti);
        }

        public ActionResult Assegna(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quesiti quesiti = db.Quesitis.Find(id);
            if (quesiti == null)
            {
                return HttpNotFound();
            }
            return View(quesiti);
        }

        [HttpPost]
        public ActionResult Assegna([Bind(Include = "Quesito_Id,DataI,DataF,Casa_Id,Cognome,NomeAlunno")] AssegnaUserViewModel assegna)
        {
            if (ModelState.IsValid)
            {
                Quesiti quesiti = db.Quesitis.Find(assegna.Quesito_Id);
                quesiti.Casa_Id = assegna.Casa_Id;
                quesiti.DataF = DateTime.Now;
                quesiti.NomeAlunno = assegna.NomeAlunno;
                quesiti.Cognome = assegna.Cognome;
                db.Entry(quesiti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AssegnaTornei", "Punteggis", new {casa =quesiti.Casa_Id, punti = quesiti.Valore, insegnante = quesiti.Prof, datat = quesiti.DataI, torneoId = quesiti.Quesito_Id});
            }
            return View(assegna);
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

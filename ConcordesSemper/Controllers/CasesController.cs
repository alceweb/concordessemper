using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConcordesSemper.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;

namespace ConcordesSemper.Controllers
{
    public class CasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, DateFormatString = "dd/MMM/yy" };


        // GET: Cases
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(db.Cases.ToList());
        }

        public ActionResult Content(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            //DataView per grafico giornaliero 
            ViewBag.DataPoints = JsonConvert.SerializeObject(db.Punteggis
                .Where(p => p.Casa_Id == id)
                .GroupBy(d => DbFunctions.TruncateTime(d.Data))
                .Select(s => new { x = s.Key, y = s.Sum(p => p.Comportamento + p.Dimenticanze + p.OeP + p.Varie + p.GrandiG) })
                .OrderBy(s=>s.x)
                .ToList(), _jsonSetting);
            //punteggioTotOggi generale casa
            var punteggio = db.Punteggis.Where(p => p.Casa_Id == id).OrderByDescending(p=>p.Data).ToList();
            ViewBag.Punteggio = punteggio;
            //lista di tutti i punteggi assegnati di oggi
            var punteggioOggi = punteggio.Where(p => p.Data >= DateTime.Today);
                ViewBag.PunteggioOggi = punteggioOggi;
            ViewBag.CountOggi = punteggioOggi.Count();
            //punteggio Totale Oggi
            var punteggioTotOggi = punteggioOggi.Sum(p => p.Comportamento) + punteggioOggi.Sum(p => p.Dimenticanze) + punteggioOggi.Sum(p => p.OeP) + punteggioOggi.Sum(p => p.Varie) + punteggioOggi.Sum(p => p.GrandiG);
            ViewBag.PunteggioTotOggi = punteggioTotOggi;
           var alunni = db.Alunnis.Where(c=>c.Casa_Id == id).OrderBy(a => a.Cognome).ToList();
            var incaricati = db.Users.Where(t => t.Casa_Id == id).OrderBy(o=>o.UserName).ToList();
            ViewBag.Incaricati = incaricati;
            ViewBag.Alunni = alunni;
            return View(@case);
        }

        // GET: Cases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // GET: Cases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cases/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Casa_Id,Nome")] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Cases.Add(@case);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@case);
        }

        // GET: Cases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Casa_Id,Nome")] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@case);
        }

        // GET: Cases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Cases.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Case @case = db.Cases.Find(id);
            db.Cases.Remove(@case);
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

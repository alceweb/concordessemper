using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcordesSemper.Models;

namespace ConcordesSemper.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var casate = db.Cases.ToList();
            var punti = db.Cases.GroupBy(a => a.Nome).
            Select(a => new {
                casa = a.Key,
                punti = a.Sum(p => p.Punteggis.Sum(s => s.Punti)) +
            a.Sum(p => p.Punteggis.Sum(s => s.Comportamento)) +
            a.Sum(p => p.Punteggis.Sum(s => s.Dimenticanze)) +
            a.Sum(p => p.Punteggis.Sum(s => s.Varie)) +
            a.Sum(p => p.Punteggis.Sum(s => s.GrandiG)) +
            a.Sum(p => p.Punteggis.Sum(s => s.OeP))
            }).
            OrderByDescending(o => o.punti).Max(o=>o.punti);
            ViewBag.AC = casate.Where(c => c.Casa_Id < 3);
            ViewBag.FF = casate.Where(c => c.Casa_Id >= 3);
            ViewBag.Punti = punti;
            return View(casate);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
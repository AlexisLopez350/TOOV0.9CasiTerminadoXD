using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using blankspaces.Models;

namespace blankspaces.Controllers
{
    public class DETALLEDEPERSONAsController : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();

        // GET: DETALLEDEPERSONAs
        public ActionResult Index()
        {
            return View(db.DETALLEDEPERSONAs.ToList());
        }

        // GET: DETALLEDEPERSONAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLEDEPERSONA dETALLEDEPERSONA = db.DETALLEDEPERSONAs.Find(id);
            if (dETALLEDEPERSONA == null)
            {
                return HttpNotFound();
            }
            return View(dETALLEDEPERSONA);
        }

        // GET: DETALLEDEPERSONAs/Create
        public ActionResult Create()
        {



            var sql = "SELECT COUNT(*) FROM dbo.DETALLEDEPERSONA";
            var total = db.Database.SqlQuery<int>(sql).First();
            ViewBag.prueba = total + 1;





            return View();
        }

        // POST: DETALLEDEPERSONAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDETALLE,LUGARDEESTUDIOS,TRABAJA,ESTUDIA,NOMBREMADRE,NOMBREPADRE")] DETALLEDEPERSONA dETALLEDEPERSONA)
        {
            if (ModelState.IsValid)
            {
                db.DETALLEDEPERSONAs.Add(dETALLEDEPERSONA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dETALLEDEPERSONA);
        }

        // GET: DETALLEDEPERSONAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLEDEPERSONA dETALLEDEPERSONA = db.DETALLEDEPERSONAs.Find(id);
            if (dETALLEDEPERSONA == null)
            {
                return HttpNotFound();
            }
            return View(dETALLEDEPERSONA);
        }

        // POST: DETALLEDEPERSONAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDETALLE,LUGARDEESTUDIOS,TRABAJA,ESTUDIA,NOMBREMADRE,NOMBREPADRE")] DETALLEDEPERSONA dETALLEDEPERSONA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dETALLEDEPERSONA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dETALLEDEPERSONA);
        }

        // GET: DETALLEDEPERSONAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLEDEPERSONA dETALLEDEPERSONA = db.DETALLEDEPERSONAs.Find(id);
            if (dETALLEDEPERSONA == null)
            {
                return HttpNotFound();
            }
            return View(dETALLEDEPERSONA);
        }

        // POST: DETALLEDEPERSONAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DETALLEDEPERSONA dETALLEDEPERSONA = db.DETALLEDEPERSONAs.Find(id);
            db.DETALLEDEPERSONAs.Remove(dETALLEDEPERSONA);
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

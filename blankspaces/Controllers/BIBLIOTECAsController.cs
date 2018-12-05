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
    public class BIBLIOTECAsController : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();

        // GET: BIBLIOTECAs
        public ActionResult Index()
        {
            return View(db.BIBLIOTECAs.ToList());
        }

        // GET: BIBLIOTECAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            if (bIBLIOTECA == null)
            {
                return HttpNotFound();
            }
            return View(bIBLIOTECA);
        }

        // GET: BIBLIOTECAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BIBLIOTECAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBIBLIOTECA,NOMBRE,DIRECCION,TELEFONO")] BIBLIOTECA bIBLIOTECA)
        {
            if (ModelState.IsValid)
            {
                db.BIBLIOTECAs.Add(bIBLIOTECA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bIBLIOTECA);
        }

        // GET: BIBLIOTECAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            if (bIBLIOTECA == null)
            {
                return HttpNotFound();
            }
            return View(bIBLIOTECA);
        }

        // POST: BIBLIOTECAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBIBLIOTECA,NOMBRE,DIRECCION,TELEFONO")] BIBLIOTECA bIBLIOTECA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bIBLIOTECA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bIBLIOTECA);
        }

        // GET: BIBLIOTECAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            if (bIBLIOTECA == null)
            {
                return HttpNotFound();
            }
            return View(bIBLIOTECA);
        }

        // POST: BIBLIOTECAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            db.BIBLIOTECAs.Remove(bIBLIOTECA);
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

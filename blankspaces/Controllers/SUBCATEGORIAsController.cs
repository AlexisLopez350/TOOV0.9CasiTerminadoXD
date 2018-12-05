using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using blankspaces.Models;
using blankspaces.ViewModels;
using Microsoft.AspNet.Identity;

namespace blankspaces.Controllers
{
    public class SUBCATEGORIAsController : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();

        // GET: SUBCATEGORIAs
        public ActionResult Index()
        {
            CategoriaViewModel cm = new CategoriaViewModel();
            cm.ID = User.Identity.GetUserId();
            var sUBCATEGORIAs = db.SUBCATEGORIAs.Include(s => s.CATERGORIA);
            return View(sUBCATEGORIAs.ToList());
        }

        // GET: SUBCATEGORIAs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBCATEGORIA sUBCATEGORIA = db.SUBCATEGORIAs.Find(id);
            if (sUBCATEGORIA == null)
            {
                return HttpNotFound();
            }
            return View(sUBCATEGORIA);
        }

        // GET: SUBCATEGORIAs/Create
        public ActionResult Create()
        {
            ViewBag.IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "IDCATEGORIA");
            return View();
        }

        // POST: SUBCATEGORIAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSUBCATEGORIA,DESCRIPCION,IDCATEGORIA")] SUBCATEGORIA sUBCATEGORIA)
        {
            if (ModelState.IsValid)
            {
                db.SUBCATEGORIAs.Add(sUBCATEGORIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "ID", sUBCATEGORIA.IDCATEGORIA);
            return View(sUBCATEGORIA);
        }

        // GET: SUBCATEGORIAs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBCATEGORIA sUBCATEGORIA = db.SUBCATEGORIAs.Find(id);
            if (sUBCATEGORIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "IDCATEGORIA", sUBCATEGORIA.IDCATEGORIA);
            return View(sUBCATEGORIA);
        }

        // POST: SUBCATEGORIAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSUBCATEGORIA,DESCRIPCION,IDCATEGORIA")] SUBCATEGORIA sUBCATEGORIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUBCATEGORIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "IDCATEGORIA", sUBCATEGORIA.IDCATEGORIA);
            return View(sUBCATEGORIA);
        }

        // GET: SUBCATEGORIAs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBCATEGORIA sUBCATEGORIA = db.SUBCATEGORIAs.Find(id);
            if (sUBCATEGORIA == null)
            {
                return HttpNotFound();
            }
            return View(sUBCATEGORIA);
        }

        // POST: SUBCATEGORIAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SUBCATEGORIA sUBCATEGORIA = db.SUBCATEGORIAs.Find(id);
            db.SUBCATEGORIAs.Remove(sUBCATEGORIA);
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

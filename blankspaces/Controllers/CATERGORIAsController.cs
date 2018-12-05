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
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace blankspaces.Controllers
{
    public class CATERGORIAsController : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();


        // GET: CATERGORIAs
        public ActionResult Index()
        {
            var cATERGORIAs = db.CATERGORIAs.Include(c => c.AspNetUser);
            return View(cATERGORIAs.ToList());
        }

        // GET: CATERGORIAs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATERGORIA cATERGORIA = db.CATERGORIAs.Find(id);
            if (cATERGORIA == null)
            {
                return HttpNotFound();
            }
            return View(cATERGORIA);
        }

        // GET: CATERGORIAs/Create
        public ActionResult Create()
        {
            CategoriaViewModel cm = new CategoriaViewModel();
            cm.ID = User.Identity.GetUserId();
            return View(cm);
        }

        // POST: CATERGORIAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCATEGORIA,ID,DESCRIPCION")] CATERGORIA cATERGORIA)
        {
            if (ModelState.IsValid)
            {
                db.CATERGORIAs.Add(cATERGORIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email", cATERGORIA.ID);
            return View(cATERGORIA);
        }

        // GET: CATERGORIAs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATERGORIA cATERGORIA = db.CATERGORIAs.Find(id);
            if (cATERGORIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.AspNetUsers, "Email", "Email", cATERGORIA.ID);
            ViewBag.CAT_IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "IDCATEGORIA");
            return View(cATERGORIA);
        }

        // POST: CATERGORIAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCATEGORIA,ID,DESCRPCION")] CATERGORIA cATERGORIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATERGORIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email", cATERGORIA.ID);
            ViewBag.CAT_IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "ID");
            return View(cATERGORIA);
        }

        // GET: CATERGORIAs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATERGORIA cATERGORIA = db.CATERGORIAs.Find(id);
            if (cATERGORIA == null)
            {
                return HttpNotFound();
            }
            return View(cATERGORIA);
        }

        // POST: CATERGORIAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CATERGORIA cATERGORIA = db.CATERGORIAs.Find(id);
            db.CATERGORIAs.Remove(cATERGORIA);
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

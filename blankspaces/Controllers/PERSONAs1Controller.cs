using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using blankspaces.Models;
using System.Web.Routing;

namespace blankspaces.Controllers
{
    public class PERSONAs1Controller : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();

        // GET: PERSONAs1
        public ActionResult Index()
        {
            var pERSONAs = db.PERSONAs.Include(p => p.MUNICIPIO);
            return View(pERSONAs.ToList());
        }

        // GET: PERSONAs1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONAs.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

        // GET: PERSONAs1/Create
        public ActionResult Create()
        {
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIOs, "IDMUNICIPIO", "NOMMUN");
            return View();
        }

        // POST: PERSONAs1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPERSONA,IDMUNICIPIO,NOMBRE,APELLIDO,FECHANACIMIENTO,GENERO,DIRECCION,TELEFONO")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {
                db.PERSONAs.Add(pERSONA);
                db.SaveChanges();
                return RedirectToAction("Register", new RouteValueDictionary(new { Controller = "Account", Action = "Register", Id = pERSONA.IDPERSONA }));
            }

            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIOs, "IDMUNICIPIO", "NOMMUN", pERSONA.IDMUNICIPIO);
            return View(pERSONA);
        }

        // GET: PERSONAs1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONAs.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIOs, "IDMUNICIPIO", "NOMMUN", pERSONA.IDMUNICIPIO);
            return View(pERSONA);
        }

        // POST: PERSONAs1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPERSONA,IDMUNICIPIO,NOMBRE,APELLIDO,FECHANACIMIENTO,GENERO,DIRECCION,TELEFONO")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSONA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIOs, "IDMUNICIPIO", "NOMMUN", pERSONA.IDMUNICIPIO);
            return View(pERSONA);
        }

        // GET: PERSONAs1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONAs.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

        // POST: PERSONAs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PERSONA pERSONA = db.PERSONAs.Find(id);
            db.PERSONAs.Remove(pERSONA);
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

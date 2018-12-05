using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using blankspaces.Models;
using blankspaces.ViewModels;

namespace blankspaces.Controllers
{
    public class PERSONAsController : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();

        // GET: PERSONAs
        public ActionResult Index()
        {
            var pERSONAs = db.PERSONAs.Include(p => p.MUNICIPIO);
            return View(pERSONAs.ToList());
        }

        public JsonResult IsUserNameAvailable(string IDPERSONA)
        {
            return Json(!db.PERSONAs.Any(person => person.IDPERSONA == IDPERSONA), JsonRequestBehavior.AllowGet);
        }

        // GET: PERSONAs/Details/5
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

        // GET: PERSONAs/Create
     

        // CAMBIOS!
        public ActionResult Crear()

        {
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIOs, "IDMUNICIPIO", "NOMMUN");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "Persona1, Detalledepersona1")] UsuarioViewModel Usuariovm)
        {

            if (Usuariovm.Detalledepersona1.LUGARDEESTUDIOS != null || Usuariovm.Detalledepersona1.NOMBREMADRE != null || Usuariovm.Detalledepersona1.NOMBREPADRE != null || Usuariovm.Detalledepersona1.ESTUDIA == true || Usuariovm.Detalledepersona1.TRABAJA == true)

            {
                //Usuariovm.Persona1.IDDETALLE = total;
                if (ModelState.IsValid)
                {

                    db.PERSONAs.Add(Usuariovm.Persona1);
                    db.SaveChanges();

                    Usuariovm.Detalledepersona1.IDPERSONA = Usuariovm.Persona1.IDPERSONA;//posible error

                    db.DETALLEDEPERSONAs.Add(Usuariovm.Detalledepersona1);
                    db.SaveChanges();
                    //changes
                   
                    //changes

                    
                    //return RedirectToAction("Index");
                    // return RedirectToAction("Create", "USUARIOs", pERSONA.IDPERSONA);
                    //TRABAJAR CON EL OBJETO PERSONA, O QUITARLO
                    return RedirectToAction("Register", new RouteValueDictionary(new { Controller = "Account", Action = "Register", Id = Usuariovm.Persona1.IDPERSONA }));
          
                  }


            }


            if (ModelState.IsValid)
            {

                // db.DETALLEDEPERSONAs.Add(Usuariovm.Detalledepersona1);
                // db.SaveChanges();

                db.PERSONAs.Add(Usuariovm.Persona1);
                db.SaveChanges();
                //return RedirectToAction("Index");
                // return RedirectToAction("Create", "USUARIOs", pERSONA.IDPERSONA);
                //TRABAJAR CON EL OBJETO PERSONA, O QUITARLO
                return RedirectToAction("Register", new RouteValueDictionary(new { Controller = "Account", Action = "Register", Id = Usuariovm.Persona1.IDPERSONA }));
            }




            ViewBag.IDDETALLE = new SelectList(db.DETALLEDEPERSONAs, "IDDETALLE", "LUGARDEESTUDIOS", new { id = 5 });
            return View(Usuariovm);
        }



        //cambios

        // POST: PERSONAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPERSONA,NOMBRE,APELLIDO,FECHANACIMIENTO,IDMUNICIPIO,GENERO,DIRECCION,TELEFONO")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {
                db.PERSONAs.Add(pERSONA);
                db.SaveChanges();
                //return RedirectToAction("Index");
                // return RedirectToAction("Create", "USUARIOs", pERSONA.IDPERSONA);
                //TRABAJAR CON EL OBJETO PERSONA, O QUITARLO
                return RedirectToAction("Register", new RouteValueDictionary(new { Controller = "AccountController", Action = "Register", Id = pERSONA.IDPERSONA }));
            }

            
            return View(pERSONA);
        }

        // GET: PERSONAs/Edit/5
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
            
            return View(pERSONA);
        }

        // POST: PERSONAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPERSONA,IDDETALLE,NOMBRE,APELLIDO,FECHANACIMIENTO,GENERO,DIRECCION,TELEFONO")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSONA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(pERSONA);
        }

        // GET: PERSONAs/Delete/5
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

        // POST: PERSONAs/Delete/5
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

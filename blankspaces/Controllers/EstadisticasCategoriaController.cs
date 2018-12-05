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
namespace blankspaces
{
    public class EstadisticasCategoriaController : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();

        // GET: EstadisticasCategoria
        public ActionResult Index()
        {
            var cATERGORIAs = db.CATERGORIAs.Include(c => c.AspNetUser);
            return View(cATERGORIAs.ToList());
        }

        // GET: EstadisticasCategoria/Details/5
        public ActionResult Details(decimal id)
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

        // GET: EstadisticasCategoria/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: EstadisticasCategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCATEGORIA,ID,NOMCAT")] CATERGORIA cATERGORIA)
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

        public ActionResult LibroMasPrestado()
        {
            DbResult vm = new DbResult();
            
           
            
            List<DbResult> studentList = db.Database.SqlQuery<DbResult>("SELECT PRES.IDMATBIBLIO,MAT.ID, COUNT(MAT.IDMATBIBLIO) AS VECES,CAT.IDFROMPRESTAMO AS PRES JOIN MATERIALBIBLIOGRAFICO AS MAT ON PRES.IDMATBIBLIO = MAT.IDMATBIBLIO JOIN CATERGORIA AS CAT ON CAT.IDCATEGORIA = MAT.IDCATEGORIA GROUP BY PRES.IDMATBIBLIO, MAT.NOMBRE, MAT.IDMATBIBLIO, CAT.ID, MAT.ID ORDER BY 'VECES'").ToList();

            /*var studentList1 = db.PRESTAMOes
                               .SqlQuery("select tabla.IDMATBIBLIO  FROM ( select IDMATBIBLIO, COUNT(*) as Mat From PRESTAMO group by IDMATBIBLIO ) as tabla")
                               .ToList<PRESTAMO>();*/


            return View(studentList);
        }


        // GET: EstadisticasCategoria/Edit/5
        public ActionResult Edit(decimal id)
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
            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email", cATERGORIA.ID);
            return View(cATERGORIA);
        }

        // POST: EstadisticasCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCATEGORIA,ID,NOMCAT")] CATERGORIA cATERGORIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATERGORIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email", cATERGORIA.ID);
            return View(cATERGORIA);
        }

        // GET: EstadisticasCategoria/Delete/5
        public ActionResult Delete(decimal id)
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

        // POST: EstadisticasCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
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

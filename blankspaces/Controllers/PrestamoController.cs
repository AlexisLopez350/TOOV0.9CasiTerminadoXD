using blankspaces.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using blankspaces.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Routing;
using System.Data.Entity;

namespace blankspaces.Controllers
{
    public class PrestamoController : Controller
    {
        private BibliotecaEntities1 db = new BibliotecaEntities1();
        // GET: Prestamo
        public ActionResult Index(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrestamoViewModel PrestamoVm = new PrestamoViewModel();
            PrestamoVm.Material1 = db.MATERIALBIBLIOGRAFICOes.Find(id);

            if (PrestamoVm.Material1 == null)
            {
                return HttpNotFound();
            }

            /* var estaautenticado = User.Identity.IsAuthenticated;

             if(estaautenticado)
             {
                 var NombreUsuario = User.Identity.Name;
                 PrestamoVm.Nombre = NombreUsuario;
                 var Usuarioid = User.Identity.GetUserId();
                 PrestamoVm.Usuarioid = Usuarioid;
             }*/

            PrestamoVm.MatId = id ;
            PrestamoVm.Usuarioid = User.Identity.GetUserId();


            return View(PrestamoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PrestamoViewModel PrestamoVm)
        {
            PRESTAMO prestamo = new PRESTAMO();
            prestamo.ID = PrestamoVm.Usuarioid;
            prestamo.IDMATBIBLIO = PrestamoVm.Material1.IDMATBIBLIO;
            prestamo.FECHADEPRESTAMO = PrestamoVm.prestamo1.FECHADEPRESTAMO;
            prestamo.FECHADEENTREGA = PrestamoVm.prestamo1.FECHADEENTREGA;
            db.PRESTAMOes.Add(prestamo);
            db.SaveChanges();


            MATERIALBIBLIOGRAFICO material = new MATERIALBIBLIOGRAFICO();

            int nuevas = int.Parse(PrestamoVm.Material1.UNIDADES);
            nuevas = nuevas - 1;

            material.UNIDADES = nuevas.ToString();


            int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update MATERIALBIBLIOGRAFICO set UNIDADES = {0} where IDMATBIBLIO = {1}", material.UNIDADES, PrestamoVm.Material1.IDMATBIBLIO);

            //intento de registrar
            REGISTRO registro = new REGISTRO();
            registro.IDMATBIBLIO = prestamo.IDMATBIBLIO;
            registro.IDPRESTAMO = prestamo.IDPRESTAMO; //posible error
            registro.Id = PrestamoVm.Usuarioid;
            db.REGISTROes.Add(registro);
            db.SaveChanges();

            return RedirectToAction("PrintViewToPdf2", new RouteValueDictionary(new { Controller = "Prestamo", Action = "PrintViewToPdf2" }));


        }

        public ActionResult PrintViewToPdf2()
        {
            var report = new ActionAsPdf("Imprimir2");
            return report;
        }


        public ActionResult Imprimir2()
        {
            var sql = "select max(IDPRESTAMO) from dbo.PRESTAMO";
            var total = db.Database.SqlQuery<int>(sql).First();
            ViewBag.total = total;


            PrestamoViewModel PrestamoVm = new PrestamoViewModel();
            PrestamoVm.prestamo1 = db.PRESTAMOes.Find(total);
            var IDMATBIBLIO = PrestamoVm.prestamo1.IDMATBIBLIO;
            PrestamoVm.Material1 = db.MATERIALBIBLIOGRAFICOes.Find(IDMATBIBLIO);
            var IDUSUARIO = User.Identity.GetUserId();
            PrestamoVm.User = db.AspNetUsers.Find(IDUSUARIO);

            //var IDPERSONA = PrestamoVm.User.IdPersona;
            //PrestamoVm.Persona1 = db.PERSONAs.Find(IDPERSONA);





            return View(PrestamoVm);
        }

        public ActionResult Devolver(decimal id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrestamoViewModel PrestamoVm = new PrestamoViewModel();
            PrestamoVm.Material1 = db.MATERIALBIBLIOGRAFICOes.Find(id);




            var sql = "select max(IDPRESTAMO) from dbo.PRESTAMO where IDMATBIBLIO =" + id.ToString();
            var total = db.Database.SqlQuery<int>(sql).First();
            ViewBag.total = total;



            PrestamoVm.prestamo1 = db.PRESTAMOes.Find(total);


            var IDUSUARIO = PrestamoVm.prestamo1.ID;
            PrestamoVm.User = db.AspNetUsers.Find(IDUSUARIO);

            var IDPERSONA = PrestamoVm.User.IdPersona;
            PrestamoVm.Persona1 = db.PERSONAs.Find(IDPERSONA);

            return View(PrestamoVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Devolver(PrestamoViewModel PrestamoVm)
        {
            var idmaterial = PrestamoVm.Material1.IDMATBIBLIO;

            int nuevas = int.Parse(PrestamoVm.Material1.UNIDADES);
            nuevas = nuevas + 1;
            var nuevasString = nuevas.ToString();


            int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update MATERIALBIBLIOGRAFICO set UNIDADES = {0} where IDMATBIBLIO = {1}", nuevasString, idmaterial);

            return RedirectToAction("Index2", new RouteValueDictionary(new { Controller = "MATERIALBIBLIOGRAFICOes", Action = "Index2" }));
        }
    }
}
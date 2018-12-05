using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using blankspaces.Models;
using blankspaces.ViewModels;
using System.Data;
using System.Data.Entity;
using PagedList;
using System.Net;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace blankspaces.Controllers
{
    public class MATERIALBIBLIOGRAFICOesController : Controller
    {


        // GET: Material

        private BibliotecaEntities1 db = new BibliotecaEntities1();


        // GET: MATERIALBIBLIOGRAFICOes
        public ActionResult Index(string sortOrder, string searchString, string CurrentFilter, int? page)
        { // cambios borre el objeto catergorias, no se porque ya no se creo.
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewBag.AutorSortParm = String.IsNullOrEmpty(sortOrder) ? "Autor" : "";
            var mATERIALBIBLIOGRAFICOes = db.MATERIALBIBLIOGRAFICOes.Include(m => m.CATERGORIA).Include(m => m.DOCUMENTOLOCALIDAD).Include(m => m.TIPODOCUMENTO).Include(m => m.SUBCATEGORIA);

            //desde aqui es el codigo para la busqueda por filtro 
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {

                mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.Where(s => s.NOMBRE.Contains(searchString) || s.DESCRIPCION.Contains(searchString) || s.SINOPSIS.Contains(searchString) ||
                s.EDITORIAL.Contains(searchString) || s.LENGUAJE.Contains(searchString) || s.AUTOR.NOMBRE.Contains(searchString) || s.CATERGORIA.IDCATEGORIA.Contains(searchString) || s.SUBCATEGORIA.IDSUBCATEGORIA.Contains(searchString) || s.TIPODOCUMENTO.TIPODOCUMENTO1.Contains(searchString) || s.BIBLIOTECA.NOMBRE.Contains(searchString));
            }
            //hasta aqui

            switch (sortOrder)
            {
                case "Nombre":
                    mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.OrderByDescending(s => s.NOMBRE);
                    break;
                case "Autor":
                    mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.OrderByDescending(s => s.AUTOR.NOMBRE);
                    break;
                default:
                    mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.OrderBy(s => s.NOMBRE);
                    break;
            }
            //hasta aqui
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(mATERIALBIBLIOGRAFICOes.ToPagedList(pageNumber, pageSize));
        }

        // GET: MATERIALBIBLIOGRAFICOes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATERIALBIBLIOGRAFICO mATERIALBIBLIOGRAFICO = db.MATERIALBIBLIOGRAFICOes.Find(id);
            if (mATERIALBIBLIOGRAFICO == null)
            {
                return HttpNotFound();
            }
            return View(mATERIALBIBLIOGRAFICO);
        }

        public ActionResult Create()

        {
            MaterialViewModel Materialvm = new MaterialViewModel();

            //Pasar las categorias disponibles a la vista:
            ViewBag.Categorias = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "IDCATEGORIA");
            ViewBag.Tipo = new SelectList(db.TIPODOCUMENTOes, "IDTIPO", "TIPODOCUMENTO1");
            ViewBag.CategoriaList = new SelectList(GetCategoriaList(), "IDCATEGORIA", "IDCATEGORIA");
            ViewBag.Biblioteca = new SelectList(db.BIBLIOTECAs, "IDBIBLIOTECA", "NOMBRE");
            Materialvm.ID = User.Identity.GetUserId();
            

            return View(Materialvm);
        }

        public ActionResult Index2(string sortOrder, string searchString, string CurrentFilter, int? page)
        { // cambios borre el objeto catergorias, no se porque ya no se creo.
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nombre" : "";
            ViewBag.AutorSortParm = String.IsNullOrEmpty(sortOrder) ? "Autor" : "";
            
            var mATERIALBIBLIOGRAFICOes = db.MATERIALBIBLIOGRAFICOes.Include(m => m.CATERGORIA).Include(m => m.DOCUMENTOLOCALIDAD).Include(m => m.TIPODOCUMENTO).Include(m => m.SUBCATEGORIA);

            //desde aqui es el codigo para la busqueda por filtro 
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {

                mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.Where(s => s.NOMBRE.Contains(searchString) || s.DESCRIPCION.Contains(searchString) || s.SINOPSIS.Contains(searchString) ||
                s.EDITORIAL.Contains(searchString) || s.LENGUAJE.Contains(searchString) || s.AUTOR.NOMBRE.Contains(searchString) || s.CATERGORIA.IDCATEGORIA.Contains(searchString) || s.SUBCATEGORIA.IDSUBCATEGORIA.Contains(searchString) || s.TIPODOCUMENTO.TIPODOCUMENTO1.Contains(searchString) || s.BIBLIOTECA.NOMBRE.Contains(searchString));
            }
            //hasta aqui

            switch (sortOrder)
            {
                case "Nombre":
                    mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.OrderByDescending(s => s.NOMBRE);
                    break;
                case "Autor":
                    mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.OrderByDescending(s => s.AUTOR.NOMBRE);
                    break;
                default:
                    mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.OrderBy(s => s.NOMBRE);
                    break;
            }
            //hasta aqui
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(mATERIALBIBLIOGRAFICOes.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaterialViewModel Materialvm, HttpPostedFileBase videofile, HttpPostedFileBase videofile1)
        {
            var id = checkauthor(Materialvm.Autor1);

            if (id == 0)
            {
                db.AUTORs.Add(Materialvm.Autor1);
                db.SaveChanges();
                Materialvm.MaterialBibliografico1.IDAUTOR = (int)Materialvm.Autor1.IDAUTOR;
                

            }

            else
            {
                Materialvm.MaterialBibliografico1.IDAUTOR = id;

            }

            //Materialvm.Relationship_15.IDAUTOR = (int)Materialvm.MaterialBibliografico1.IDAUTOR; //Agregado para insertar en taba relationship 15

            //Materialvm.MaterialBibliografico1.IDCATEGORIA = Materialvm.Categoria1.IDCATEGORIA;


            //Unir el autor agregado al material


            DOCUMENTOLOCALIDAD doc = new DOCUMENTOLOCALIDAD();
            if (videofile != null)
            {
                string filename = Path.GetFileName(videofile.FileName);
                videofile.SaveAs(Server.MapPath("/Materiales/" + filename));
                doc.LOCACIONOURL = "Materiales/" + filename;
                db.DOCUMENTOLOCALIDADs.Add(doc);
                db.SaveChanges();

                ViewData["Message"] = "Record Saved Succesfully";

            }

            if (doc.LOCACIONOURL != null)
            { Materialvm.MaterialBibliografico1.IDLOCALIDAD = doc.IDLOCALIDAD; }


            //Carga de imagenes
            DOCUMENTOLOCALIDAD doc1 = new DOCUMENTOLOCALIDAD();

            if (videofile1 != null)
            {
                string filename = Path.GetFileName(videofile1.FileName);
                videofile1.SaveAs(Server.MapPath("/Imagenes/" + filename));
                Materialvm.MaterialBibliografico1.FOTO = "Imagenes/" + filename;
                //db.DOCUMENTOLOCALIDADs.Add(doc1);
                //db.SaveChanges();

                ViewData["Message"] = "Record Saved Succesfully";


            }

            //Guardar material
            Materialvm.ID = User.Identity.GetUserId();
            Materialvm.MaterialBibliografico1.ID = Materialvm.ID;


            Materialvm.MaterialBibliografico1.IDLOCALIDAD = 1;


            db.MATERIALBIBLIOGRAFICOes.Add(Materialvm.MaterialBibliografico1);

            db.SaveChanges();

            return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "MATERIALBIBLIOGRAFICOes", Action = "Index" }));


        }



        /*public JsonResult GetSubCategoriaList(int IDCATEGORIA)
        {
           db.Configuration.ProxyCreationEnabled = false;
            List<SUBCATEGORIA> CategoriaList = db.SUBCATEGORIAs.Where(x => x.IDCATEGORIA == 1 ).ToList();
            return Json(CategoriaList, JsonRequestBehavior.AllowGet);
        }
            */
        public List<CATERGORIA> GetCategoriaList()
        {
            BibliotecaEntities1 sd = new BibliotecaEntities1();
            List<CATERGORIA> categorias = sd.CATERGORIAs.ToList();
            return categorias;
        }

        public ActionResult GetSubcaList(string IDCATEGORIA)
        {
            BibliotecaEntities1 sd = new BibliotecaEntities1();
            List<SUBCATEGORIA> selectList = sd.SUBCATEGORIAs.Where(x => x.IDCATEGORIA == IDCATEGORIA).ToList();
            ViewBag.Slist = new SelectList(selectList, "IDSUBCATEGORIA", "IDSUBCATEGORIA");
            return PartialView("DisplaySubcategoria");

        }

        public int checkauthor(AUTOR autor)
        {
            List<AUTOR> autores = new List<AUTOR>();
            autores = db.AUTORs.ToList();
            var id = 0;


            foreach (AUTOR variable in autores)
            {
                if (autor.NOMBRE.ToUpper() == variable.NOMBRE.ToUpper())
                {
                    id = (int)variable.IDAUTOR;
                    break;


                }


            }

            return id;

        }
        public ActionResult Edit(decimal id)
        {
            MaterialViewModel Materialvm = new MaterialViewModel();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATERIALBIBLIOGRAFICO mATERIALBIBLIOGRAFICO = db.MATERIALBIBLIOGRAFICOes.Find(id);
            ViewBag.Biblioteca = new SelectList(db.BIBLIOTECAs, "IDBIBLIOTECA", "NOMBRE");

            Materialvm.MaterialBibliografico1 = mATERIALBIBLIOGRAFICO;

            Materialvm.foto = mATERIALBIBLIOGRAFICO.FOTO;//added
            if (mATERIALBIBLIOGRAFICO.IDSUBCATEGORIA != null)
            {
                Materialvm.subcategory = mATERIALBIBLIOGRAFICO.IDSUBCATEGORIA;
            }



            if (mATERIALBIBLIOGRAFICO.IDLOCALIDAD != null)
            {
                Materialvm.idlocalidad = (int)mATERIALBIBLIOGRAFICO.IDLOCALIDAD;
            }


            if (Materialvm.MaterialBibliografico1.IDAUTOR != null)
            {
                AUTOR autor = db.AUTORs.Find(Materialvm.MaterialBibliografico1.IDAUTOR);
                Materialvm.Autor1 = autor;
            }

            ViewBag.CountryList = new SelectList(GetCategoriaList(), "IDCATEGORIA", "IDCATEGORIA");

            if (Materialvm.MaterialBibliografico1.IDCATEGORIA != null)
            {
                CATERGORIA categoria = db.CATERGORIAs.Find(Materialvm.MaterialBibliografico1.IDCATEGORIA);
                Materialvm.Categoria1 = categoria;
                Materialvm.IDCATEGORIA = Materialvm.MaterialBibliografico1.IDCATEGORIA;
            }

            if (Materialvm.MaterialBibliografico1.IDLOCALIDAD != null)
            {
                DOCUMENTOLOCALIDAD doc = db.DOCUMENTOLOCALIDADs.Find(Materialvm.MaterialBibliografico1.IDLOCALIDAD);
                Materialvm.DocumentoLocalidad1 = doc;
            }



            ViewBag.Tipo = new SelectList(db.TIPODOCUMENTOes, "IDTIPO", "TIPODOCUMENTO1");



            //WELL PROBABLY HAVE BUGS CAUSE MANY ATRIBUTES ARE NULL.


            if (mATERIALBIBLIOGRAFICO == null)
            {
                return HttpNotFound();
            }


            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email", mATERIALBIBLIOGRAFICO.ID);
            ViewBag.IDAUTOR = new SelectList(db.AUTORs, "IDAUTOR", "NOMBRE", mATERIALBIBLIOGRAFICO.IDAUTOR);
            ViewBag.IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "ID", mATERIALBIBLIOGRAFICO.IDCATEGORIA);
            ViewBag.IDLOCALIDAD = new SelectList(db.DOCUMENTOLOCALIDADs, "IDLOCALIDAD", "LOCACIONOURL", mATERIALBIBLIOGRAFICO.IDLOCALIDAD);
            ViewBag.IDTIPO = new SelectList(db.TIPODOCUMENTOes, "IDTIPO", "TIPODOCUMENTO1", mATERIALBIBLIOGRAFICO.IDTIPO);
            ViewBag.IDSUBCATEGORIA = new SelectList(db.SUBCATEGORIAs, "IDSUBCATEGORIA", "NOMBRE", mATERIALBIBLIOGRAFICO.IDSUBCATEGORIA);
            return View(Materialvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaterialViewModel Materialvm, HttpPostedFileBase videofile, HttpPostedFileBase videofile1)
        {
            Materialvm.MaterialBibliografico1.FOTO = Materialvm.foto; //posible error
            Materialvm.MaterialBibliografico1.IDLOCALIDAD = Materialvm.idlocalidad;
            // Materialvm.MaterialBibliografico1.IDSUBCATEGORIA = Materialvm.subcategory;


            MATERIALBIBLIOGRAFICO mATERIALBIBLIOGRAFICO = new MATERIALBIBLIOGRAFICO();
            mATERIALBIBLIOGRAFICO = Materialvm.MaterialBibliografico1;

            if (mATERIALBIBLIOGRAFICO.IDSUBCATEGORIA == null)
            {
                mATERIALBIBLIOGRAFICO.IDSUBCATEGORIA = Materialvm.subcategory;
            }


            var id = checkauthor(Materialvm.Autor1);

            if (id == 0)
            {
                db.AUTORs.Add(Materialvm.Autor1);
                db.SaveChanges();
                Materialvm.MaterialBibliografico1.IDAUTOR = (int)Materialvm.Autor1.IDAUTOR;

            }

            else
            {
                Materialvm.MaterialBibliografico1.IDAUTOR = id;

            }

            //Archivo digital


            DOCUMENTOLOCALIDAD doc = new DOCUMENTOLOCALIDAD();
            if (videofile != null)
            {
                string filename = Path.GetFileName(videofile.FileName);
                videofile.SaveAs(Server.MapPath("/Materiales/" + filename));
                doc.LOCACIONOURL = "Materiales/" + filename;
                db.DOCUMENTOLOCALIDADs.Add(doc);
                db.SaveChanges();

                ViewData["Message"] = "Record Saved Succesfully";

            }

            if (doc.LOCACIONOURL != null)
            {
                Materialvm.MaterialBibliografico1.IDLOCALIDAD = doc.IDLOCALIDAD;
                mATERIALBIBLIOGRAFICO.IDLOCALIDAD = Materialvm.MaterialBibliografico1.IDLOCALIDAD;
            }

            DOCUMENTOLOCALIDAD doc1 = new DOCUMENTOLOCALIDAD();



            if (videofile1 != null)
            {
                string filename = Path.GetFileName(videofile1.FileName);
                videofile1.SaveAs(Server.MapPath("/Imagenes/" + filename));
                Materialvm.MaterialBibliografico1.FOTO = "Imagenes/" + filename;
                mATERIALBIBLIOGRAFICO.FOTO = Materialvm.MaterialBibliografico1.FOTO;
                //db.DOCUMENTOLOCALIDADs.Add(doc1);
                //db.SaveChanges();

                ViewData["Message"] = "Record Saved Succesfully";


            }


            if (ModelState.IsValid)
            {
                db.Entry(mATERIALBIBLIOGRAFICO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.AspNetUsers, "Id", "Email", mATERIALBIBLIOGRAFICO.ID);
            ViewBag.IDAUTOR = new SelectList(db.AUTORs, "IDAUTOR", "NOMBRE", mATERIALBIBLIOGRAFICO.IDAUTOR);
            ViewBag.IDCATEGORIA = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "ID", mATERIALBIBLIOGRAFICO.IDCATEGORIA);
            ViewBag.IDLOCALIDAD = new SelectList(db.DOCUMENTOLOCALIDADs, "IDLOCALIDAD", "LOCACIONOURL", mATERIALBIBLIOGRAFICO.IDLOCALIDAD);
            ViewBag.IDTIPO = new SelectList(db.TIPODOCUMENTOes, "IDTIPO", "TIPODOCUMENTO1", mATERIALBIBLIOGRAFICO.IDTIPO);
            ViewBag.IDSUBCATEGORIA = new SelectList(db.SUBCATEGORIAs, "IDSUBCATEGORIA", "NOMBRE", mATERIALBIBLIOGRAFICO.IDSUBCATEGORIA);
            return View(mATERIALBIBLIOGRAFICO);
        }

        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATERIALBIBLIOGRAFICO mATERIALBIBLIOGRAFICO = db.MATERIALBIBLIOGRAFICOes.Find(id);
            if (mATERIALBIBLIOGRAFICO == null)
            {
                return HttpNotFound();
            }
            return View(mATERIALBIBLIOGRAFICO);
        }

        // POST: MATERIALBIBLIOGRAFICOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MATERIALBIBLIOGRAFICO mATERIALBIBLIOGRAFICO = db.MATERIALBIBLIOGRAFICOes.Find(id);
            db.MATERIALBIBLIOGRAFICOes.Remove(mATERIALBIBLIOGRAFICO);
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
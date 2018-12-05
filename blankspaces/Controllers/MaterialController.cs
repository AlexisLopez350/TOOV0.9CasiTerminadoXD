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

namespace blankspaces.Controllers
{
    public class MaterialController : Controller
    {


        // GET: Material

        private BibliotecEntities db = new BibliotecEntities();


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
                mATERIALBIBLIOGRAFICOes = mATERIALBIBLIOGRAFICOes.Where(s => s.NOMBRE.Contains(searchString) || s.DESCRIPCION.Contains(searchString) || s.SINOPSIS.Contains(searchString) || s.UNIDADES.Contains(searchString)
                || s.EDITORIAL.Contains(searchString) || s.LENGUAJE.Contains(searchString) || s.AUTOR.NOMBRE.Contains(searchString));
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

        public ActionResult Crear()

        {
            MaterialViewModel Materialvm = new MaterialViewModel();

            //Pasar las categorias disponibles a la vista:
            ViewBag.Categorias = new SelectList(db.CATERGORIAs, "IDCATEGORIA", "NOMCAT");
            ViewBag.Tipo = new SelectList(db.TIPODOCUMENTOes, "IDTIPO", "TIPODOCUMENTO1");
            ViewBag.CountryList = new SelectList(GetCountryList(), "IDCATEGORIA", "NOMCAT");

            return View(Materialvm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(MaterialViewModel Materialvm, HttpPostedFileBase videofile, HttpPostedFileBase videofile1)
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
            db.MATERIALBIBLIOGRAFICOes.Add(Materialvm.MaterialBibliografico1);
            
            db.SaveChanges();

           return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "Material", Action = "Index" }));


        }

        /*public JsonResult GetSubCategoriaList(int IDCATEGORIA)
        {
           db.Configuration.ProxyCreationEnabled = false;
            List<SUBCATEGORIA> CategoriaList = db.SUBCATEGORIAs.Where(x => x.IDCATEGORIA == 1 ).ToList();
            return Json(CategoriaList, JsonRequestBehavior.AllowGet);
        }
            */
        public List<CATERGORIA> GetCountryList()
        {
            BibliotecEntities sd = new BibliotecEntities();
            List<CATERGORIA> countries = sd.CATERGORIAs.ToList();
            return countries;
        }

        public ActionResult GetStateList(int IDCATEGORIA)
        {
            BibliotecEntities sd = new BibliotecEntities();
            List<SUBCATEGORIA> selectList = sd.SUBCATEGORIAs.Where(x => x.IDCATEGORIA == IDCATEGORIA).ToList();
            ViewBag.Slist = new SelectList(selectList, "IDSUBCATEGORIA", "NOMBRE");
            return PartialView("DisplayStates");

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

    }
}
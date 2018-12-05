using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blankspaces.Models;

namespace blankspaces.ViewModels
{
    public class MaterialViewModel
    {
        public AUTOR Autor1 { get; set; }
        public CATERGORIA Categoria1 { get; set; }
        public SUBCATEGORIA Subcategoria1 { get; set; }
        public DOCUMENTOLOCALIDAD DocumentoLocalidad1 { get; set; }
        public MATERIALBIBLIOGRAFICO MaterialBibliografico1 { get; set; }
        public TIPODOCUMENTO TipoDocumento1 { get; set; }
        public BIBLIOTECA BIBLIOTECA1 { get; set; }
        public string IDCATEGORIA { get; set; }
        public string IDSUBCATEGORIA { get; set; }
        public string foto { get; set; }
        public int idlocalidad { get; set; }
        public string subcategory { get; set; }
        public string ID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blankspaces.Models;
using System.ComponentModel.DataAnnotations;

namespace blankspaces.ViewModels
{
    public class CategoriaViewModel
    {
        public CATERGORIA CAT1 { get; set; }
        [Required]
        [Display(Name = "Nombre Categoria")]
        public string IDCATEGORIA { get; set; }
        [Required]
        [Display(Name = "Usuario")]
        public string ID { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

    }
}
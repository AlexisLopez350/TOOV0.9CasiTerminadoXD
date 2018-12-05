using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blankspaces.Models
{
    public class EstadisticasLibros
    {
        public string materiaBibliografico { set; get; }
        public string nombreLibro { set; get; }
        public int veces { set; get; }
        public string nombreCategoria { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blankspaces.Models;

namespace blankspaces.ViewModels
{
    public class PrestamoViewModel
    {
        public MATERIALBIBLIOGRAFICO Material1 { get; set; }
        public int MatId { get; set; }
        public string Usuarioid { get; set; }
        public PRESTAMO prestamo1 { get; set; }
        public AspNetUser User { get; set; }
        public PERSONA Persona1 { get; set; }
    }
}
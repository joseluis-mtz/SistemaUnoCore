using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsSedes
    {
        [Display(Name = "Id Sede")]
        public int iidSede { get; set; }
        [Display(Name = "Nombre Sede")]
        public string nombreSede { get; set; }
        [Display(Name = "Dirección Sede")]
        public string direccion { get; set; }
    }
}

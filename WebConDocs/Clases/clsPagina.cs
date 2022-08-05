using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsPagina
    {
        [Display(Name = "ID Página")]
        public int iidpagina { get; set; }
        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }
        [Display(Name = "Acción")]
        public string accion { get; set; }
        [Display(Name = "Controller")]
        public string controller { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsSedes
    {
        [Display(Name = "Id Sede")]
        public int iidSede { get; set; }


        [Display(Name = "Nombre Sede")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string nombreSede { get; set; }
        
        
        [Display(Name = "Dirección Sede")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string direccion { get; set; }
    }
}

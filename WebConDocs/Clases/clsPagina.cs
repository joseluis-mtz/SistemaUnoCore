using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsPagina
    {
        [Display(Name = "ID Página")]
        public int iidpagina { get; set; }


        [Display(Name = "Mensaje")]
        [Required(ErrorMessage = "Ingrese el mensaje.")]
        public string mensaje { get; set; }
        
        
        [Display(Name = "Acción")]
        [Required(ErrorMessage = "Ingrese la acción.")]
        public string accion { get; set; }
        
        
        [Display(Name = "Controller")]
        [Required(ErrorMessage = "Ingrese el controlador.")]
        [MinLength(3,ErrorMessage = "Longitud mínima de 3 caracteres.")]
        [MaxLength(100,ErrorMessage = "Longitud máxima de 100 caracteres.")]
        public string controller { get; set; }

        public string MensajeError { get; set; }
    }
}

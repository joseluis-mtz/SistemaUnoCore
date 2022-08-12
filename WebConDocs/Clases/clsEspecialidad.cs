using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsEspecialidad
    {
        [Display(Name = "Id Especialidad")]
        public int iidespecialidad { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="El nombre es obligatorio.")]
        public string nombre { get; set; }
        
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string descripcion { get; set; }
    }
}

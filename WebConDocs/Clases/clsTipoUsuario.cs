using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsTipoUsuario
    {
        [Display(Name ="Id Tipo Usuario")]
        public int iidTipoUsuario { get; set; }


        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string nombre { get; set; }


        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string descripcion { get; set; }

        public string MensajeErrorNombre { get; set; }
        public string MensajeErrorDescripcion { get; set; }

    }
}

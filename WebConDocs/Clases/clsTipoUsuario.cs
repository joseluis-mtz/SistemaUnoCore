using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsTipoUsuario
    {
        [Display(Name ="Id Tipo Usuario")]
        public int iidTipoUsuario { get; set; }
        [Display(Name = "Nombre de Usuario")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

    }
}

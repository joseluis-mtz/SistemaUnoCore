using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsPersona
    {
        [Display(Name = "ID Persona")]
        public int iidPersona { get; set; }
        [Display(Name = "Nombre Completo")]
        public string nombreCompleto { get; set; }
        [Display(Name = "Correo")]
        public string email { get; set; }
        [Display(Name = "Sexo")]
        public string nombreSexo { get; set; }
        public int iidSexo { get; set; }
    }
}

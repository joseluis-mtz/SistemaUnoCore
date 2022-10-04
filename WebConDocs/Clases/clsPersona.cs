using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsPersona
    {
        [Display(Name = "ID Persona")]
        public int? iidPersona { get; set; }


        [Display(Name = "Nombre Completo")]
        public string? nombreCompleto { get; set; }


        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress,ErrorMessage ="El correo debe ser valido")]
        public string email { get; set; }


        [Display(Name = "Sexo")]
        public string? nombreSexo { get; set; }


        // Propiedades adicionales
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string nombre { get; set; }


        [Display(Name = "Apellido paterno")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string aPaterno { get; set; }


        [Display(Name = "Apellido materno")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string aMaterno { get; set; }

        
        [Required(ErrorMessage ="Campo obligatorio")]
        [MinLength(10, ErrorMessage ="Debe ser de 10 digitos")]
        [MaxLength(10, ErrorMessage = "Debe ser de 10 digitos")]
        [Display(Name = "Teléfono fijo")]
        public string telefonoFijo { get; set; }


        [Display(Name = "Teléfono celular")]
        public string telefonoCelular { get; set; }


        [Display(Name = "Fecha nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "El formato de fecha debe ser valido")]
        [DisplayFormat(DataFormatString ="0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage ="Campo obligatorio")]
        public DateTime? fechaNacimiento { get; set; }


        [Display(Name = "Seleccionar Sexo")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int? iidSexo { get; set; }


        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string direccion { get; set; }

        public string MensajeError { get; set; }
    }
}

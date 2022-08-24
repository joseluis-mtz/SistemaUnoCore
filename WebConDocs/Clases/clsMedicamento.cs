using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsMedicamento
    {
        [Display(Name = "ID Medicamento")]
        public int iidmedicamento { get; set; }
        
        
        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Ingrese el nombre del medicamento.")]
        public string nombre { get; set; }
        
        
        
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Ingrese el precio del medicamento.")]
        public decimal? precio { get; set; }
        
        
        
        [Display(Name = "Stock")]
        [Required(ErrorMessage = "Ingrese el stock del medicamento.")]
        [Range(0, 1000, ErrorMessage = "De 0 a 1000 solamente.")]
        public int? stock { get; set; }
        
        
        
        [Display(Name = "Nombre Forma Farmaceutica")]
        // Está propiedad no estaba mapeando con el modelo por lo tanto no registraba un estado Valido en el modelo
        // Tuve que ponerla de manera opcional con ? para poder hacer válido el módelo
        public string? nombreFormaFarmaceutica { get; set; }
        
        
        
        [Display(Name = "Selecciona forma farmaceutica")]
        [Required(ErrorMessage = "Ingrese la forma farmaceutica.")]
        public int? iidFormaFarmaceutica { get; set; }
        
        
        
        [Display(Name = "Concentración")]
        [Required(ErrorMessage = "Ingrese la concentración.")]
        public string concentracion { get; set; }
        
        
        
        [Display(Name = "Presentación")]
        [Required(ErrorMessage = "Ingrese la presentación.")]
        public string presentacion { get; set; }
    }
}

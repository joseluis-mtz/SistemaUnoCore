using System.ComponentModel.DataAnnotations;

namespace WebConDocs.Clases
{
    public class clsMedicamento
    {
        [Display(Name = "ID Medicamento")]
        public int iidmedicamento { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        [Display(Name = "Stock")]
        public int stock { get; set; }
        [Display(Name = "Forma Farmaceutica")]
        public string nombreFormaFarmaceutica { get; set; }
        public int iidFormaFarmaceutica { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebConDocs.Clases;
using WebConDocs.Models;

namespace WebConDocs.Controllers
{
    public class MedicamentoController : Controller
    {

        public List<SelectListItem> ListaFormasFarmaceuticas()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                Lista = (from farma in db.FormaFarmaceuticas
                        where farma.Bhabilitado == 1
                        select new SelectListItem { 
                            Text = farma.Nombre,
                            Value = farma.Iidformafarmaceutica.ToString()
                        }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "Seleccionar", Value = "" });
            }
                return Lista;
        }
        public IActionResult Index(clsMedicamento objMedicina)
        {
            ViewBag.listaForma = ListaFormasFarmaceuticas();
            List<clsMedicamento> lista = new List<clsMedicamento>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                if (objMedicina.iidFormaFarmaceutica == 0)
                {
                    lista = (from medicina in db.Medicamentos
                             join forma in db.FormaFarmaceuticas
                             on medicina.Iidformafarmaceutica equals forma.Iidformafarmaceutica
                             where medicina.Bhabilitado == 1
                             select new clsMedicamento
                             {
                                 iidmedicamento = medicina.Iidmedicamento,
                                 nombre = medicina.Nombre,
                                 precio = (decimal)medicina.Precio,
                                 stock = (int)medicina.Stock,
                                 nombreFormaFarmaceutica = forma.Nombre
                             }).ToList();
                }
                else
                {
                    lista = (from medicina in db.Medicamentos
                             join forma in db.FormaFarmaceuticas
                             on medicina.Iidformafarmaceutica equals forma.Iidformafarmaceutica
                             where medicina.Bhabilitado == 1
                             && medicina.Iidformafarmaceutica == objMedicina.iidFormaFarmaceutica
                             select new clsMedicamento
                             {
                                 iidmedicamento = medicina.Iidmedicamento,
                                 nombre = medicina.Nombre,
                                 precio = (decimal)medicina.Precio,
                                 stock = (int)medicina.Stock,
                                 nombreFormaFarmaceutica = forma.Nombre
                             }).ToList();
                }
            }
                return View(lista);
        }
    }
}

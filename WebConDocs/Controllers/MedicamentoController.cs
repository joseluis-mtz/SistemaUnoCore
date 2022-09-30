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
                if (objMedicina.iidFormaFarmaceutica == 0 || objMedicina.iidFormaFarmaceutica == null)
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

        public IActionResult Registrar()
        {
            ViewBag.ListaFormas = ListaFormasFarmaceuticas();
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(clsMedicamento objMedicamento)
        {
            string nombreVista = "";
            try
            {
                if (objMedicamento.iidmedicamento == 0)
                {
                    nombreVista = "Registrar";
                }
                else
                {
                    nombreVista = "Editar";
                }
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.ListaFormas = ListaFormasFarmaceuticas();
                        return View(nombreVista,objMedicamento);
                    }
                    else
                    {
                        if (objMedicamento.iidmedicamento == 0)
                        {
                            Medicamento nuevoMed = new Medicamento();
                            nuevoMed.Nombre = objMedicamento.nombre;
                            nuevoMed.Concentracion = objMedicamento.concentracion;
                            nuevoMed.Iidformafarmaceutica = objMedicamento.iidFormaFarmaceutica;
                            nuevoMed.Precio = objMedicamento.precio;
                            nuevoMed.Stock = objMedicamento.stock;
                            nuevoMed.Presentacion = objMedicamento.presentacion;
                            nuevoMed.Bhabilitado = 1;
                            db.Medicamentos.Add(nuevoMed);
                            db.SaveChanges();
                        }
                        else
                        {
                            Medicamento editarMed = db.Medicamentos.Where(x => x.Iidmedicamento == objMedicamento.iidmedicamento).First();
                            editarMed.Nombre = objMedicamento.nombre;
                            editarMed.Concentracion = objMedicamento.concentracion;
                            editarMed.Iidformafarmaceutica = objMedicamento.iidFormaFarmaceutica;
                            editarMed.Precio = objMedicamento.precio;
                            editarMed.Stock = objMedicamento.stock;
                            editarMed.Presentacion = objMedicamento.presentacion;
                            db.SaveChanges();
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                return View(nombreVista,objMedicamento);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int iidmedicamento)
        {
            clsMedicamento objMedicamento = new clsMedicamento();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                objMedicamento = (from medicamentos in db.Medicamentos
                                                 where medicamentos.Iidmedicamento == iidmedicamento
                                                 select new clsMedicamento
                                                 {
                                                     iidmedicamento = medicamentos.Iidmedicamento,
                                                     nombre = medicamentos.Nombre,
                                                     precio = medicamentos.Precio,
                                                     stock = medicamentos.Stock,
                                                     concentracion = medicamentos.Concentracion,
                                                     presentacion = medicamentos.Presentacion,
                                                     iidFormaFarmaceutica = medicamentos.Iidformafarmaceutica
                                                 }).First();
            }
            ViewBag.ListaFormas = ListaFormasFarmaceuticas();
            return View(objMedicamento);
        }
    }
}

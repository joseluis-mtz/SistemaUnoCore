using Microsoft.AspNetCore.Mvc;
using WebConDocs.Models;
using WebConDocs.Clases;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebConDocs.Controllers
{
    public class PersonaController : Controller
    {
        public void LlenarComboSexo()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                lista = (from elemento in db.Sexos
                         where elemento.Bhabilitado == 1
                         select new SelectListItem
                         {
                             Text = elemento.Nombre,
                             Value = elemento.Iidsexo.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "SELECCIONAR", Value = "0" });
                ViewBag.listaSexos = lista;
            }
        }
        public IActionResult Index(clsPersona objPersona)
        {
            LlenarComboSexo();
            List<clsPersona> lista = new List<clsPersona>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                if (objPersona.iidSexo == 0)
                {
                    lista = (from persona in db.Personas
                             join sexos in db.Sexos
                             on persona.Iidsexo equals sexos.Iidsexo
                             where persona.Bhabilitado == 1
                             select new clsPersona
                             {
                                 iidPersona = persona.Iidpersona,
                                 nombreCompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 email = persona.Email,
                                 nombreSexo = sexos.Nombre
                             }).ToList();
                }
                else
                {
                    lista = (from persona in db.Personas
                             join sexos in db.Sexos
                             on persona.Iidsexo equals sexos.Iidsexo
                             where persona.Bhabilitado == 1
                             && persona.Iidsexo == objPersona.iidSexo
                             select new clsPersona
                             {
                                 iidPersona = persona.Iidpersona,
                                 nombreCompleto = persona.Nombre + " " + persona.Appaterno + " " + persona.Apmaterno,
                                 email = persona.Email,
                                 nombreSexo = sexos.Nombre
                             }).ToList();
                }
                
            }
                return View(lista);
        }
    }
}

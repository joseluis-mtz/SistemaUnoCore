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
                if (objPersona.iidSexo == 0 || objPersona.iidSexo == null)
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

        public IActionResult Agregar()
        {
            LlenarComboSexo();
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(clsPersona objPersona)
        {
            LlenarComboSexo();
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(objPersona);
                }
                else
                {
                    using (BDHospitalContext db = new BDHospitalContext())
                    {
                        Persona nuevaPersona = new Persona();
                        nuevaPersona.Nombre = objPersona.nombre;
                        nuevaPersona.Appaterno = objPersona.aPaterno;
                        nuevaPersona.Apmaterno = objPersona.aMaterno;
                        nuevaPersona.Telefonofijo = objPersona.telefonoFijo;
                        nuevaPersona.Telefonocelular = objPersona.telefonoCelular;
                        nuevaPersona.Fechanacimiento = objPersona.fechaNacimiento;
                        nuevaPersona.Direccion = objPersona.direccion;
                        nuevaPersona.Email = objPersona.email;
                        nuevaPersona.Iidsexo = objPersona.iidSexo;
                        nuevaPersona.Bhabilitado = 1;
                        db.Personas.Add(nuevaPersona);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return View(objPersona);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int iidPersona)
        {
            string mensaje = "";
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    Persona objPersona = db.Personas.Where(x => x.Iidpersona == iidPersona).First();
                    objPersona.Bhabilitado = 0;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}

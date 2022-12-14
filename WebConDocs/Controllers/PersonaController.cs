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
                                 nombreSexo = sexos.Nombre,
                                 fechaNacimiento = persona.Fechanacimiento
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
        public IActionResult Guardar(clsPersona objPersona)
        {
            string NombreVista = "";
            int VecesRepetidas = 0;
            int VecesRepetidasCorreo = 0;
            if (objPersona.iidPersona == 0)
            {
                NombreVista = "Agregar";
            }
            else
            {
                NombreVista = "Editar";
            }

            LlenarComboSexo();
            
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    objPersona.nombreCompleto = objPersona.nombre.Trim().ToUpper() + " " + objPersona.aPaterno.Trim().ToUpper() + " " + objPersona.aMaterno.Trim().ToUpper();
                    if (objPersona.iidPersona == 0)
                    {
                        VecesRepetidas = db.Personas.
                            Where(x => x.Nombre.Trim().ToUpper() + " " + x.Appaterno.Trim().ToUpper() + " " + x.Apmaterno.Trim().ToUpper() == objPersona.nombreCompleto).Count();
                        VecesRepetidasCorreo = db.Personas.Where(x => x.Email.Trim() == objPersona.email.Trim()).Count();
                    }
                    else
                    {
                        VecesRepetidas = db.Personas.
                            Where(x => x.Nombre.Trim().ToUpper() + " " + x.Appaterno.Trim().ToUpper() + " " + x.Apmaterno.Trim().ToUpper() == objPersona.nombreCompleto 
                            && x.Iidpersona != objPersona.iidPersona).Count();

                        VecesRepetidasCorreo = db.Personas.Where(x => x.Email.Trim() == objPersona.email.Trim() 
                        && x.Iidpersona != objPersona.iidPersona).Count();
                    }

                    if (!ModelState.IsValid || VecesRepetidas >=1 || VecesRepetidasCorreo >= 1)
                    {
                        if (VecesRepetidas >= 1)
                        {
                            objPersona.MensajeError = "El nombre ya existe.";
                        }

                        if (VecesRepetidasCorreo >= 1)
                        {
                            objPersona.MensajeErrorEmail = "El correo ya existe.";
                        }
                        return View(NombreVista, objPersona);
                    }
                    else
                    {
                        if (objPersona.iidPersona == 0)
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
                        else
                        {
                            Persona editarPersona = db.Personas.Where(x => x.Iidpersona == objPersona.iidPersona).First();
                            editarPersona.Nombre = objPersona.nombre;
                            editarPersona.Appaterno = objPersona.aPaterno;
                            editarPersona.Apmaterno = objPersona.aMaterno;
                            editarPersona.Telefonofijo = objPersona.telefonoFijo;
                            editarPersona.Telefonocelular = objPersona.telefonoCelular;
                            editarPersona.Fechanacimiento = objPersona.fechaNacimiento;
                            editarPersona.Direccion = objPersona.direccion;
                            editarPersona.Email = objPersona.email;
                            editarPersona.Iidsexo = objPersona.iidSexo;
                            db.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception)
            {
                return View(NombreVista,objPersona);
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

        public IActionResult Editar(int iidPersona)
        {
            string mensaje = "";
            LlenarComboSexo();
            clsPersona objPersona = new clsPersona();
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    objPersona = (from personas in db.Personas
                                 where personas.Iidpersona == iidPersona
                                  select new clsPersona
                                 {
                                     iidPersona = personas.Iidpersona,
                                     nombre = personas.Nombre,
                                     aPaterno = personas.Appaterno,
                                     aMaterno = personas.Apmaterno,
                                     email = personas.Email,
                                     direccion = personas.Direccion,
                                     telefonoFijo = personas.Telefonofijo,
                                     telefonoCelular = personas.Telefonocelular,
                                     fechaNacimiento = personas.Fechanacimiento == null ? DateTime.Now : personas.Fechanacimiento,
                                     iidSexo = personas.Iidsexo
                                  }).First();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return RedirectToAction("Index");
            }
            return View(objPersona);
        }
    }
}

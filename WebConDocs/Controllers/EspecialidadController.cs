using Microsoft.AspNetCore.Mvc;
using WebConDocs.Models;
using WebConDocs.Clases;

namespace WebConDocs.Controllers
{
    public class EspecialidadController : Controller
    {
        public IActionResult Index(clsEspecialidad ObjEspecialidad)
        {
            List<clsEspecialidad> lista = new List<clsEspecialidad>();

            using (BDHospitalContext db = new BDHospitalContext())
            {
                if (string.IsNullOrEmpty(ObjEspecialidad.nombre))
                {
                    lista = (from fila in db.Especialidads
                             where fila.Bhabilitado == 1
                             select new clsEspecialidad
                             {
                                 iidespecialidad = fila.Iidespecialidad,
                                 nombre = fila.Nombre,
                                 descripcion = fila.Descripcion
                             }).ToList();
                    ViewBag.Cadena = "";
                }
                else
                {
                    lista = (from fila in db.Especialidads
                             where fila.Bhabilitado == 1
                             && fila.Nombre.Contains(ObjEspecialidad.nombre)
                             select new clsEspecialidad
                             {
                                 iidespecialidad = fila.Iidespecialidad,
                                 nombre = fila.Nombre,
                                 descripcion = fila.Descripcion
                             }).ToList();
                    ViewBag.Cadena = ObjEspecialidad.nombre;
                }
                
            }
                return View(lista);
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Registrar(clsEspecialidad objEspecialidad)
        public IActionResult Guardar(clsEspecialidad objEspecialidad)
        {
            string nombreVista = "";
            int RepeticionNombre = 0;
            try
            {
                if (objEspecialidad.iidespecialidad == 0)
                {
                    nombreVista = "Registrar";
                }
                else
                {
                    nombreVista = "Editar";
                }
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    // Contar las repeticiones del nombre
                    if (objEspecialidad.iidespecialidad == 0)
                    {
                        RepeticionNombre = db.Especialidads.Where(x => x.Nombre.ToUpper() == objEspecialidad.nombre.ToUpper()).Count();
                    }

                    if (!ModelState.IsValid || RepeticionNombre >= 1)
                    {
                        if (RepeticionNombre >= 1)
                        {
                            objEspecialidad.MensajeError = "El nombre ya existe.";
                        }
                        return View(nombreVista, objEspecialidad);
                    }
                    else
                    {
                        if (objEspecialidad.iidespecialidad == 0)
                        {
                            // Si ID = 0 entonces agregar
                            Especialidad objInsertar = new Especialidad();
                            objInsertar.Nombre = objEspecialidad.nombre;
                            objInsertar.Descripcion = objEspecialidad.descripcion;
                            objInsertar.Bhabilitado = 1;
                            db.Especialidads.Add(objInsertar);
                            db.SaveChanges();
                        }
                        else
                        {
                            // Si ID diferente de 0 entonces editar
                            Especialidad objActualizar = db.Especialidads.Where(x => x.Iidespecialidad == objEspecialidad.iidespecialidad).First();
                            objActualizar.Nombre = objEspecialidad.nombre;
                            objActualizar.Descripcion = objEspecialidad.descripcion;
                            db.SaveChanges();
                        }
                        
                    }
                }
            }
            catch (Exception Ex)
            {
                return View(nombreVista, objEspecialidad);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int iidespecialidad)
        {
            string error = "";
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    Especialidad objEspecialidad = db.Especialidads.Where(x => x.Iidespecialidad == iidespecialidad).First();
                    objEspecialidad.Bhabilitado = 0;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return View();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Editar(int iidespecialidad)
        {
            string mensaje = "";
            clsEspecialidad objEspecialidad = new clsEspecialidad();
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    objEspecialidad = (from especialidad in db.Especialidads
                                       where especialidad.Iidespecialidad == iidespecialidad
                                       select new clsEspecialidad
                                       {
                                           iidespecialidad = especialidad.Iidespecialidad,
                                           nombre = especialidad.Nombre,
                                           descripcion = especialidad.Descripcion
                                       }).First();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return RedirectToAction("Index");
            }
            return View(objEspecialidad);
        }
    }
}

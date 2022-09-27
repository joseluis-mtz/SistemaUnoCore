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
        public IActionResult Registrar(clsEspecialidad objEspecialidad)
        {
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View(objEspecialidad);
                    }
                    else
                    {
                        Especialidad objInsertar = new Especialidad();
                        objInsertar.Nombre = objEspecialidad.nombre;
                        objInsertar.Descripcion = objEspecialidad.descripcion;
                        objInsertar.Bhabilitado = 1;
                        db.Especialidads.Add(objInsertar);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception Ex)
            {
                return View(objEspecialidad);
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
    }
}

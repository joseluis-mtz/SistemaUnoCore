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
    }
}

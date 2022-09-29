using Microsoft.AspNetCore.Mvc;
using WebConDocs.Clases;
using WebConDocs.Models;

namespace WebConDocs.Controllers
{
    public class SedeController : Controller
    {
        public IActionResult Index(clsSedes objSede)
        {
            List<clsSedes> lista = new List<clsSedes>();
            using (BDHospitalContext bd = new BDHospitalContext())
            {
                if (string.IsNullOrEmpty(objSede.nombreSede))
                {
                    lista = (from sede in bd.Sedes
                             where sede.Bhabilitado == 1
                             select new clsSedes
                             {
                                 iidSede = sede.Iidsede,
                                 nombreSede = sede.Nombre,
                                 direccion = sede.Direccion
                             }).ToList();
                    ViewBag.Cadena = "";
                }
                else
                {
                    lista = (from sede in bd.Sedes
                             where sede.Bhabilitado == 1
                             && sede.Nombre.Contains(objSede.nombreSede)
                             select new clsSedes
                             {
                                 iidSede = sede.Iidsede,
                                 nombreSede = sede.Nombre,
                                 direccion = sede.Direccion
                             }).ToList();
                    ViewBag.Cadena = objSede.nombreSede;
                }
            }
                return View(lista);
        }

        public IActionResult Eliminar(int iidSede)
        {
            string mensaje = "";
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    Sede objSede = db.Sedes.Where(x => x.Iidsede == iidSede).First();
                    objSede.Bhabilitado = 0;
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

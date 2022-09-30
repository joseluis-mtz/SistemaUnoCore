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
        public IActionResult Registrar()
        {
            return View();
        }
        public IActionResult Guardar(clsSedes objSede)
        {
            string nombreVista = "";
            try
            {
                if (objSede.iidSede == 0)
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
                        return View(nombreVista, objSede);
                    }
                    else
                    {
                        if (objSede.iidSede == 0)
                        {
                            // Si ID = 0 entonces agregar
                            Sede objInsertar = new Sede();
                            objInsertar.Nombre = objSede.nombreSede;
                            objInsertar.Direccion = objSede.direccion;
                            objInsertar.Bhabilitado = 1;
                            db.Sedes.Add(objInsertar);
                            db.SaveChanges();
                        }
                        else
                        {
                            // Si ID diferente de 0 entonces editar
                            Sede objActualizar = db.Sedes.Where(x => x.Iidsede == objSede.iidSede).First();
                            objActualizar.Nombre = objSede.nombreSede;
                            objActualizar.Direccion = objSede.direccion;
                            db.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception)
            {
                return View(nombreVista, objSede);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int iidSede)
        {
            string mensaje = "";
            clsSedes objSede = new clsSedes();
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    objSede = (from sedes in db.Sedes
                                       where sedes.Iidsede == iidSede
                                       select new clsSedes
                                       {
                                           iidSede = sedes.Iidsede,
                                           nombreSede = sedes.Nombre,
                                           direccion = sedes.Direccion
                                       }).First();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return RedirectToAction("Index");
            }
            return View(objSede);
        }
    }
}

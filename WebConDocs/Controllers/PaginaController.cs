using Microsoft.AspNetCore.Mvc;
using WebConDocs.Clases;
using WebConDocs.Models;
namespace WebConDocs.Controllers
{
    public class PaginaController : Controller
    {
        public IActionResult Index(clsPagina objPagina)
        {
            List<clsPagina> lista = new List<clsPagina>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                if (string.IsNullOrEmpty(objPagina.mensaje))
                {
                    lista = (from pagina in db.Paginas
                             where pagina.Bhabilitado == 1
                             select new clsPagina
                             {
                                 iidpagina = pagina.Iidpagina,
                                 mensaje = pagina.Mensaje,
                                 accion = pagina.Accion,
                                 controller = pagina.Controlador
                             }).ToList();
                    ViewBag.mensaje = "";
                }
                else
                {
                    lista = (from pagina in db.Paginas
                             where pagina.Bhabilitado == 1
                             && pagina.Mensaje.Contains(objPagina.mensaje)
                             select new clsPagina
                             {
                                 iidpagina = pagina.Iidpagina,
                                 mensaje = pagina.Mensaje,
                                 accion = pagina.Accion,
                                 controller = pagina.Controlador
                             }).ToList();
                    ViewBag.mensaje = objPagina.mensaje;
                }
            }
                return View(lista);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(clsPagina paPagina)
        {
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View(paPagina);
                    }
                    else
                    {
                        Pagina NuevaPagina = new Pagina();
                        NuevaPagina.Mensaje = paPagina.mensaje;
                        NuevaPagina.Accion = paPagina.accion;
                        NuevaPagina.Controlador = paPagina.controller;
                        NuevaPagina.Bhabilitado = 1;
                        db.Paginas.Add(NuevaPagina);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                return View(paPagina);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int iidpagina)
        {
            string mensaje = "";
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    Pagina objPagina = db.Paginas.Where(x => x.Iidpagina == iidpagina).First();
                    db.Paginas.Remove(objPagina);
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

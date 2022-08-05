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
    }
}

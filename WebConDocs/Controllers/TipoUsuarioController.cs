using Microsoft.AspNetCore.Mvc;
using WebConDocs.Clases;
using WebConDocs.Models;

namespace WebConDocs.Controllers
{
    public class TipoUsuarioController : Controller
    {
        public IActionResult Index(clsTipoUsuario objTipoUsuario)
        {
            List<clsTipoUsuario> lista = new List<clsTipoUsuario>();
            using (BDHospitalContext db = new BDHospitalContext())
            {
                lista = (from tipo in db.TipoUsuarios
                         where tipo.Bhabilitado == 1
                         select new clsTipoUsuario
                         {
                             iidTipoUsuario = tipo.Iidtipousuario,
                             nombre = tipo.Nombre,
                             descripcion = tipo.Descripcion
                         }).ToList();

                if (objTipoUsuario.nombre == null && objTipoUsuario.descripcion == null && objTipoUsuario.iidTipoUsuario == 0)
                {
                    ViewBag.iidTipoUsuario = 0;
                    ViewBag.nombre = "";
                    ViewBag.descripcion = "";
                }
                else
                {
                    if (objTipoUsuario.nombre != null)
                    {
                        lista = lista.Where(p=>p.nombre.Contains(objTipoUsuario.nombre)).ToList();
                    }
                    if (objTipoUsuario.descripcion != null)
                    {
                        lista = lista.Where(p => p.descripcion.Contains(objTipoUsuario.descripcion)).ToList();
                    }
                    if (objTipoUsuario.iidTipoUsuario != 0)
                    {
                        lista = lista.Where(p => p.iidTipoUsuario == objTipoUsuario.iidTipoUsuario).ToList();
                    }

                    ViewBag.iidTipoUsuario = objTipoUsuario.iidTipoUsuario;
                    ViewBag.nombre = objTipoUsuario.nombre;
                    ViewBag.descripcion = objTipoUsuario.descripcion;
                }
            }
            return View(lista);
        }
    }
}

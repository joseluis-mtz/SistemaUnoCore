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
        public IActionResult Guardar(clsPagina paPagina)
        {
            string nombreVista = "";
            int VecesRepetidas = 0;
            try
            {
                if (paPagina.iidpagina == 0)
                {
                    nombreVista = "Agregar";
                }
                else
                {
                    nombreVista = "Editar";
                }

                using (BDHospitalContext db = new BDHospitalContext())
                {
                    if (paPagina.iidpagina == 0)
                    {
                        VecesRepetidas = db.Paginas.Where(x => x.Mensaje.Trim().ToUpper() == paPagina.mensaje.Trim().ToUpper()).Count();
                    }
                    else
                    {
                        VecesRepetidas = db.Paginas.Where(x => x.Mensaje.Trim().ToUpper() == paPagina.mensaje.Trim().ToUpper()
                        && x.Iidpagina != paPagina.iidpagina).Count();
                    }

                    if (!ModelState.IsValid || VecesRepetidas>=1)
                    {
                        var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                        if (VecesRepetidas >= 1)
                        {
                            paPagina.MensajeError = "El mensaje ya existe.";
                        }
                        
                        return View(nombreVista,paPagina);
                    }
                    else
                    {
                        if (paPagina.iidpagina == 0)
                        {
                            Pagina NuevaPagina = new Pagina();
                            NuevaPagina.Mensaje = paPagina.mensaje;
                            NuevaPagina.Accion = paPagina.accion;
                            NuevaPagina.Controlador = paPagina.controller;
                            NuevaPagina.Bhabilitado = 1;
                            db.Paginas.Add(NuevaPagina);
                            db.SaveChanges();
                        }
                        else
                        {
                            Pagina EditarPagina = db.Paginas.Where(x => x.Iidpagina == paPagina.iidpagina).First();
                            EditarPagina.Mensaje = paPagina.mensaje;
                            EditarPagina.Accion = paPagina.accion;
                            EditarPagina.Controlador = paPagina.controller;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return View(nombreVista,paPagina);
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

        public IActionResult Editar(int iidpagina)
        {
            string mensaje = "";
            clsPagina objPagina = new clsPagina();
            try
            {
                using (BDHospitalContext db = new BDHospitalContext())
                {
                    objPagina = (from paginas in db.Paginas
                                       where paginas.Iidpagina == iidpagina
                                 select new clsPagina
                                       {
                                           iidpagina = paginas.Iidpagina,
                                           mensaje = paginas.Mensaje,
                                           accion = paginas.Accion,
                                           controller = paginas.Controlador
                                       }).First();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return RedirectToAction("Index");
            }
            return View(objPagina);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ContadorController : Controller
    {
        //
        // GET: /Contador/
        public ActionResult Index()
        {
            object valorContador=Session["contador"];
            int contador = Convert.ToInt32(valorContador);
            contador++;
            Session["contador"] = contador;
            return View(contador);
        }
	}
}
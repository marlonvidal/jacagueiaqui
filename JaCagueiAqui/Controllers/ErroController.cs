using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JaCagueiAqui.Controllers
{
    public class ErroController : Controller
    {
        //
        // GET: /Erro/

        public ActionResult Index()
        {
            return View("Erro");
        }

        public ActionResult NaoExiste()
        {
            return View();
        }
    }
}

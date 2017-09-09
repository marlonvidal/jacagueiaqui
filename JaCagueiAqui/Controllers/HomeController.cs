using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JaCagueiAqui.Model;
using JaCagueiAqui.Data;
using JaCagueiAqui.Negocio;
using JaCagueiAqui.Models;
using JaCagueiAqui.Helpers;

namespace JaCagueiAqui.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        int tamanhoPagina = 1;

        [HttpPost]
        public JsonResult ListarItens()
        {
            return Json(new PostBLL().ListaItens());
        }

        public ActionResult Index()
        {
            return View(new ComentarioBLL().BuscaComentarios());
        }

        public ActionResult RegistrarCagada()
        {
            return View();
        }

        public JsonResult RegistrarNaoGostei(int ID)
        {
            return Json(new PostBLL().RegistrarNaoGostei(ID));
        }

        public JsonResult RegistrarGostei(int ID)
        {
            return Json(new PostBLL().RegistrarGostei(ID));
        }

        [HttpPost]
        public ActionResult Salvar(Post post)
        {
            if (ModelState.IsValid)
            {
                new PostBLL().Salvar(post);
                TempData["Mensagem"] = "Cagada registrada com sucesso!";

                return Json(new { url = Url.Action("Index") });
            }

            return PartialView("FormularioCagada", post);
        }

        public ActionResult Cidade(string nome)
        {
            Session["Cidade"] = nome;
            return RedirectToAction("Index", new ComentarioBLL().BuscaComentarios());
        }

        public ActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalvarContato(Contato contato)
        {
            if (ModelState.IsValid)
            {
                new ContatoBLL().Salvar(contato);
                TempData["Mensagem"] = "Mensagem enviada com sucesso!";

                return RedirectToAction("Index", new ComentarioBLL().BuscaComentarios());
            }

            return View("Contato", contato);
        }

        [HttpPost]
        public ActionResult SalvarComentario(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                new ComentarioBLL().Salvar(comentario);
                TempData["Mensagem"] = "Comentario enviado com sucesso!";

                return Json(new { url = Url.Action("Index") });
            }

            return PartialView("FormularioComentario", comentario);
        }

        public ActionResult QuemFezEssaMerda()
        {
            return View();
        }

        public ActionResult Cagada(int? id)
        {
            if (id == null)
                return View("NaoEncontrado");

            var post = new PostBLL().RetornaPost(id ?? 0);
            var comentariosCagada = new ComentarioCagadaBLL().RetornaComentarios(id ?? 0);

            if (post == null)
                return View("NaoEncontrado");

            return View(new CagadaView(post, comentariosCagada));
        }

        public ActionResult TopCagadas()
        {
            List<Post> maisCurtidos = new List<Post>(),
                       maisComentados = new List<Post>();

            PostBLL bll = new PostBLL();
            int total = CalcularTotalPaginas();

            var viewMaisCurtidas = new SessaoTopCagadasView(
                bll.RetornaMaisCurtidas(tamanhoPagina), "maisCurtidos", "MaisCurtidos", total);
            
            var viewMaisComentados = new SessaoTopCagadasView(
                bll.RetornaMaisComentadas(tamanhoPagina), "maisComentados", "MaisComentados", total);

            return View(new TopCagadasView(viewMaisCurtidas, viewMaisComentados));
        }

        public ActionResult SalvarComentarioCagada(ComentarioCagada comentarioCagada)
        {
            if (ModelState.IsValid)
                new ComentarioCagadaBLL().Salvar(comentarioCagada);
                
            var comentarios = new ComentarioCagadaBLL().RetornaComentarios(comentarioCagada.PostId);

            return PartialView("ComentarioCagada", comentarios);
        }

        public ActionResult MaisComentados(int? pagina = 1)
        {
            var total = new PostBLL().RetornaTotalRegistros();

            var viewMaisComentados = new SessaoTopCagadasView(
                new PostBLL().RetornaMaisComentadas(tamanhoPagina, (int)pagina), 
                "maisComentados", "MaisComentados", CalcularTotalPaginas(), (int)pagina);

            return PartialView("SessaoTopCagadas", viewMaisComentados);
        }

        public ActionResult MaisCurtidos(int? pagina = 1)
        {
            var total = new PostBLL().RetornaTotalRegistros();

            var viewMaisCurtidas = new SessaoTopCagadasView(
                new PostBLL().RetornaMaisCurtidas(tamanhoPagina, (int)pagina), 
                "maisCurtidos", "MaisCurtidos", CalcularTotalPaginas(), (int)pagina);

            return PartialView("SessaoTopCagadas", viewMaisCurtidas);
        }

        private int CalcularTotalPaginas()
        {
            return (int)new PostBLL().RetornaTotalRegistros() / tamanhoPagina;
        }
    }
}

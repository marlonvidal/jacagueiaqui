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

        int tamanhoPagina = 20;

        [HttpPost]
        public JsonResult ListarItens()
        {
            return Json(new PostBLL().ListaItens());
        }

        public ActionResult Index()
        {
            //return View(new ComentarioBLL().BuscaComentarios());
            return View(new List<Comentario>());
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

        public ActionResult TopCagadas(string sessao = "", int pagina = 1)
        {
            var model = RetornaViewTopCagadas(pagina, sessao);

            return View(model);
        }

        public ActionResult SalvarComentarioCagada(ComentarioCagada comentarioCagada)
        {
            if (ModelState.IsValid)
                new ComentarioCagadaBLL().Salvar(comentarioCagada);

            var comentarios = new ComentarioCagadaBLL().RetornaComentarios(comentarioCagada.PostId);

            return PartialView("ComentarioCagada", comentarios);
        }

        public ActionResult MaisComentadas(int pagina = 1)
        {
            var model = RetornaViewTopCagadas(pagina, "maiscomentadas");

            return View("TopCagadas", model);
        }

        public ActionResult MaisCurtidos(int pagina = 1)
        {
            var model = RetornaViewTopCagadas(pagina, "maiscurtidas");

            return View("TopCagadas", model);
        }

        private TopCagadasView RetornaViewTopCagadas(int pagina, string sessao)
        {
            string nomeSessao = "", nomeAction = "", idDiv = "";

            PostBLL bll = new PostBLL(); List<Post> itens = new List<Post>();
            PagingInfo paging = new PagingInfo(pagina, bll.RetornaTotalRegistros(), tamanhoPagina);

            switch (sessao.ToLower())
            {
                case "maiscomentadas":
                    nomeSessao = "Veja aqui as cagadas mais comentadas!";
                    itens = bll.RetornaMaisComentadas(tamanhoPagina, pagina);
                    idDiv = "maisComentados";
                    nomeAction = "MaisComentadas";
                    break;

                case "maiscurtidas":
                default:
                    nomeSessao = "Acompanhe aqui as cagadas mais curtidas!";
                    itens = bll.RetornaMaisCurtidas(tamanhoPagina, pagina);
                    idDiv = "maisCurtidos";
                    nomeAction = "MaisCurtidos";
                    break;
            }

            var sessaoTopCagadaView = new SessaoTopCagadasView(itens, idDiv, nomeAction, paging);

            return new TopCagadasView(sessaoTopCagadaView, nomeSessao);
        }
    }
}

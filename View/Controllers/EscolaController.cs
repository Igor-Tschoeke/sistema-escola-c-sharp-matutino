using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class EscolaController : Controller
    {
        EscolaRepositorio repositorio = new EscolaRepositorio();
        
        public ActionResult Index()
        {
            List<Escola> escolas = repositorio.ObterTodos("");

            ViewBag.Escola = escolas;

            return View();
        }
        public ActionResult Store(string nome)
        {
            Escola escola = new Escola();
            escola.Nome = nome;

            int id = repositorio.Inserir(escola);
            return RedirectToAction("Cadastro Escola");
        }
    }
}
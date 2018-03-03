using AulaModel.Modelo.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AulaModel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            

            return View(Pessoa.Pessoas);
        }

        public ActionResult InserirPessoa()
        {
            return View();
        }

        public ActionResult GravarPessoa(Pessoa pessoa)
        {
            pessoa.Id = Guid.NewGuid();
            Pessoa.Pessoas.Add(pessoa);

            //return View("Index", Pessoa.Pessoas);
            return RedirectToAction("Index");
        }

        public ActionResult ApagarPessoa(Guid id)
        {
            Pessoa p = null;
            foreach(var pessoa in Pessoa.Pessoas)
            {
                if(pessoa.Id == id)
                {
                    p = pessoa;
                    break;
                }
            }

            if(p != null)
            {
                Pessoa.Pessoas.Remove(p);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Buscar(String edtBusca)
        {
            var pessoaAux = new List<Pessoa>();

            if (String.IsNullOrEmpty(edtBusca))
            {
                return View("Index", Pessoa.Pessoas);
            }

            //1 - Filtro com FOR
            //for(var i = 0; i < Pessoa.Pessoas.Count; i++)
            //{
            //    if (Pessoa.Pessoas[i].Nome == edtBusca.Trim())
            //    {
            //        pessoaAux.Add(Pessoa.Pessoas[i]);
            //    }
            //}

            //2 - Filtro com FOREACH
            //foreach(var pessoa in Pessoa.Pessoas)
            //{
            //    if(pessoa.Nome == edtBusca.Trim())
            //    {
            //        pessoaAux.Add(pessoa);
            //    }
            //}

            //3 - Filtro com LINQ
            //pessoaAux = (
            //        from p in Pessoa.Pessoas
            //        where p.Nome == edtBusca.Trim()
            //        select p
            //    ).ToList();

            //4 - Filtro com 
            pessoaAux = Pessoa.Pessoas.Where(x => 
                            x.Nome == edtBusca.Trim()
                        ).ToList();

            return View("Index", pessoaAux);
        }
    }
}
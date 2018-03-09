using AulaModel.Modelo.DB;
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
            var pessoas = DbFactory.Instance.PessoaRepository.FindAll();

            return View(pessoas);
        }

        public ActionResult InserirPessoa()
        {
            return View();
        }

        public ActionResult GravarPessoa(Pessoa pessoa)
        {
            DbFactory.Instance.PessoaRepository.SaveOrUpdate(pessoa);

            //return View("Index", Pessoa.Pessoas);
            return RedirectToAction("Index");
        }

        public ActionResult ApagarPessoa(Guid id)
        {
            var pessoa = DbFactory.Instance.PessoaRepository.FindById(id);

            if(pessoa != null) { 
                DbFactory.Instance.PessoaRepository.Delete(pessoa);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Buscar(String edtBusca)
        {
            

            if (String.IsNullOrEmpty(edtBusca))
            {
                return RedirectToAction("Index");
            }

            var pessoa = DbFactory.Instance.PessoaRepository.GetAllByName(edtBusca);

            return View("Index", pessoa);

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
            // pessoaAux = Pessoa.Pessoas.Where(x => 
            //  x.Nome == edtBusca.Trim()
            //  ).ToList();


        }

        public ActionResult EditarPessoa(Guid id)
        {
            var pessoa = DbFactory.Instance.PessoaRepository.FindById(id);

            if (pessoa != null)
            {
                return View(pessoa);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}
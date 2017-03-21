using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        //
        // GET: /Categoria/
        public ActionResult Index()
        {
            CategoriasDAO dao = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = dao.Lista();
            ViewBag.Categorias = categorias;
            return View();
        }
        public ActionResult Visualizar(int id)
        {
            CategoriasDAO dao = new CategoriasDAO();
            CategoriaDoProduto categoria = dao.BuscaPorId(id);
            ViewBag.Categorias = categoria;
            return View();
        }
        public ActionResult Remover(int id)
        {
            CategoriasDAO dao = new CategoriasDAO();
            CategoriaDoProduto categoria = dao.BuscaPorId(id);
            dao.Remover(categoria);
            return RedirectToAction("Index", "Categoria");
        }
        public ActionResult Editar(int id)
        {
            if(id==0)
            {
                CategoriaDoProduto categoria = new CategoriaDoProduto();
                ViewBag.Categorias = categoria;
            }
            if (id != 0)
            {
                CategoriasDAO dao = new CategoriasDAO();
                CategoriaDoProduto categoria = dao.BuscaPorId(id);
                ViewBag.Categorias = categoria;
            }
               return View("Form");
        }
        [HttpPost]
        public ActionResult Form()
        {
            CategoriaDoProduto categoria = new CategoriaDoProduto();
            ViewBag.Categorias = categoria;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(CategoriaDoProduto categoria)
        {
            CategoriasDAO dao = new CategoriasDAO();
            if ((categoria.Nome == "") || (categoria.Nome == null))
            {
                ModelState.AddModelError("categoria.Nome", "O Campo Usuário é obrigatório");
            }
            if ((categoria.Descricao == "") || (categoria.Descricao == null))
            {
                ModelState.AddModelError("categoria.Senha", "O Campo *Senha é obrigatório");
            }
            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    dao.Adiciona(categoria);
                }
                if(categoria.Id>0)
                {
                    dao.Atualiza(categoria);
                }

                return RedirectToAction("Index", "Categoria");
            }
            else
            {
                ViewBag.Categorias = categoria;
                return View("Form");
            }

        }
	}
}
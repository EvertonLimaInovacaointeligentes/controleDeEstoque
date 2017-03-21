using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Filtros;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
   // [AutorizacaoFilter]
    public class ProdutoController : Controller
    {
        //
        // GET: /Produto/
        /*para nao utilizar ViewBag precisamos passar na View o objeto lista de produtos diretamente na view
         e na pagina web declarar um model do mesmo tipo de lista de produtos esse tipo de procedimento se diz
         mais seguro e segue as boas praticas de programação*/
        [Route("produtos",Name="ListarProdutos")]        
        public ActionResult Index()
      {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            //ViewBag.Produtos = produtos;
            return View(produtos);
        }
        
        public ActionResult Form()
        {
            CategoriasDAO dao = new CategoriasDAO();
            IList<CategoriaDoProduto> categoria = dao.Lista();
            ViewBag.Categoria = "";
            ViewBag.Categoria = categoria;
            
            
            ViewBag.Produto = new Produto();
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Produto produto)
        {
           /* Não precisa utilizar esse parametro utilizado somente para entender como criar uma nova regra para o modelState, posso colocar direto na classe produto que está em
            * model informado que os campos preco e quantidade são obrigadotios;
            * if(produto.Preco==0)
            {
                ModelState.AddModelError("produto.Preco","O Campo: *Preço é Obrigatório!");
                
            }
            if(produto.Quantidade==0)
            {
                ModelState.AddModelError("produto.Quantidade","O Campo: *Quantidade é Obrigatório");
            }
            */
            produto.Categoria = null;
            if(produto.Preco==0)
            {
                ModelState.AddModelError("produto.precozero","O campo: Preço não pode ser 0 (zero)");
            }
            if(produto.Quantidade==0)
            {
                ModelState.AddModelError("produto.quantidadezero","O campo: Quantidade não pode ser 0 (zero)");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                //CategoriasDAO categoriadao = new CategoriasDAO();
                //int codigo = (int)produto.CategoriaId;
                //CategoriaDoProduto categoria = categoriadao.BuscaPorId(codigo);
                //produto.Categoria = categoria;
              //  ViewBag.Produtos.Categoria = categoria;
                if (produto.Id > 0)
                {
                    dao.Atualiza(produto);
                }
                if (produto.Id == 0)
                {
                    dao.Adiciona(produto);
                }
                
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                ViewBag.Produto = produto;
                
                CategoriasDAO dao = new CategoriasDAO();
                ViewBag.Categoria = dao.Lista();
                int codigo = (int)produto.CategoriaId;
                CategoriaDoProduto categoria = dao.BuscaPorId(codigo);
                produto.Categoria = categoria;
                ViewBag.CategoriaProduto = categoria.Nome;

                
                return View("Form");
            }
        }
        
        public ActionResult InicioHome()
        {
            return RedirectToAction("Index","Home");
        }
        [Route("produtos/{id}",Name="VisualizarProdutos")]
        public ActionResult Visualiza(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto=dao.BuscaPorId(id);
            ViewBag.Produto = produto;
            return View();
        }
        
        public ActionResult Editar(int id)
        {
                        
                //produto dao
                ProdutosDAO dao = new ProdutosDAO();
                //CAtegoria dao
                CategoriasDAO daoCategoria = new CategoriasDAO();
                //buscando produto pelo codigo e produto preenchido
                Produto produto = dao.BuscaPorId(id);
                //peangando a categoria pelo codigo para preencher no form a categoria salva pelo usuario vindo do banco de dados

                //pegando a categoria eslhida pelo usuario

                ViewBag.Produto = produto;
                ViewBag.Categoria = daoCategoria.Lista();
                
            
            return View("Form");
        }
        
        public ActionResult Remover(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            dao.Remover(produto);

            return RedirectToAction("Index","Produto");
        }
        /*esse metodo vai retornar um json pois as requisiçoes de envio
         vão ser realizadas via ajax de forma assemetrica*/
        public ActionResult Decrementa(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            //return RedirectToAction("Index","Produto");
            return Json(produto);
        }
	}
}
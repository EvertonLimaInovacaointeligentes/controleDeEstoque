using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.Models;
using CaelumEstoque.DAO;
namespace CaelumEstoque.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        public ActionResult Index()
        {
            UsuariosDAO dao = new UsuariosDAO();
            IList<Usuario> usuarios = dao.Lista();

            return View(usuarios);
        }
        public ActionResult Visualizar(int id)
        {
            UsuariosDAO dao = new UsuariosDAO();
            Usuario usuario = dao.BuscaPorId(id);
            ViewBag.Usuario = usuario;
            return View();
        }
        public ActionResult Remover(int id)
        {
            UsuariosDAO dao = new UsuariosDAO();
            Usuario usuario = dao.BuscaPorId(id);
            dao.Remover(usuario);
            return RedirectToAction("Index","Usuario");
        }
        public ActionResult Editar(int id)
        {
            if (id == 0)
            {
                Usuario usuario = new Usuario();
                ViewBag.Usuario = usuario;
            }
            else
            {
                UsuariosDAO dao = new UsuariosDAO();
                Usuario usuario = dao.BuscaPorId(id);
                ViewBag.Usuario = usuario;
            }
            return View("Form");
        }
        [HttpPost]
        public ActionResult Form()
        {
            Usuario usuario = new Usuario();
            ViewBag.Usuario = usuario;
            return View("Form");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Usuario usuario)
        {
            UsuariosDAO dao = new UsuariosDAO();
            if((usuario.Nome=="")||(usuario.Nome==null))
            {
                ModelState.AddModelError("usuario.Nome","O Campo Usuário é obrigatório");
            }
            if ((usuario.Senha == "") || (usuario.Senha == null))
            {
                ModelState.AddModelError("usuario.Senha","O Campo *Senha é obrigatório");
            }
            if (ModelState.IsValid)
            {
                if (usuario.Id != 0)
                {
                    dao.Atualiza(usuario);
                }
                if (usuario.Id == 0)
                {                    
                    dao.Adiciona(usuario);
                }
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                ViewBag.Usuario = usuario;                                                
                return View("Form");
            }
            
        }
	}
}
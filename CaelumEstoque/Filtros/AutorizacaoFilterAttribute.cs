﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//ActionFilterAttribute
namespace CaelumEstoque.Filtros
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {



        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object usuario = filterContext.HttpContext.Session["usuarioLogado"];
            if (usuario == null)
            {
                if (filterContext.Controller is CaelumEstoque.Controllers.LoginController)
                {
                    //Nao faz nada
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary(
                                   new { action = "Index", controller = "Login" }));
                }

                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
        }
    }
}
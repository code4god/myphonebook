using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleBlackBook.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: ErrorController
        public ActionResult Index()
        {
            return View("~/Shared/Error");
        }


    }
}

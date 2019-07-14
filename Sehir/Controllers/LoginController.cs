using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sehir.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string email, string password)
        {

            return View("Index");
        }
        [HttpPost]
        public ActionResult Register(string email, string password, string password2)
        {

            return View("Index");
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            return View("Index");
        }

    }
}
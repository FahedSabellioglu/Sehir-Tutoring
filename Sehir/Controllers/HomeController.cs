using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sehir.Controllers
{
    [Authorize(Roles = "Admin,Lecturer,HomeworkMaker")]
    public class HomeController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }
    }
}
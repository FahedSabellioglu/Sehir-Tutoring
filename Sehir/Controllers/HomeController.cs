using Sehir.App_Classes;
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
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Contribute(string mail)
        {
            try
            {
                emailSending emailObject = new emailSending("fahedshaabani@std.sehir.edu.tr");

                string Message = "Wanna contribute to the project\n" + "My email is:" + mail;

                emailObject.SendProcedure(Message, "Contribution");

                TempData["ContributionMessage"] = "the email has been shared with the admin, you will hear from as soon";
            }
            catch
            {
                TempData["ContributionMessage"] = "Failed to share the email, Please try again later";
            }

            return RedirectToAction("Main");

        }

    }
}
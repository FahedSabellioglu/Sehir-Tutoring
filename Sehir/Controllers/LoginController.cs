using System.Web.Mvc;

namespace Sehir.Controllers
{
    using Sehir.App_Classes;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    public class LoginController : Controller
    {
        
        SehirTutoringEntities cx = new SehirTutoringEntities();
        public ActionResult Index()
        {
            if (TempData["LoginMessage"] !=null)
            {
                ViewBag.message = TempData["LoginMessage"].ToString();

            }

            return View();
        }

        string ValidateUser(string ua, string pa)
        {
            User kl = cx.Users.FirstOrDefault(x => x.mail == ua && x.pass == pa);

            return kl != null ? kl.mail : "";
        }

        [HttpPost]
        public ActionResult login(User usr)
        {
            string Email = ValidateUser(usr.mail, usr.pass);
            if (!string.IsNullOrEmpty(Email))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usr.U_name, DateTime.Now, DateTime.Now.AddMinutes(60), true, Email, FormsAuthentication.FormsCookiePath);
                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName);

                if (ticket.IsPersistent)
                {
                    ck.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(ck);
                FormsAuthentication.RedirectFromLoginPage(Email, true);
            }
            else
            {
                TempData["LoginMessage"] = "Failed to Login, Please try again later.";
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Register(User UserObject)
        {
            bool Dup_email = cx.Users.Any(x => x.mail == UserObject.mail || x.id == UserObject.id);
            if (!Dup_email)
            {
                if (UserObject.img == null)
                {
                    string imgPath = Server.MapPath("~/Content/eiser/img/default_user.jpg");
                    UserObject.img = System.IO.File.ReadAllBytes(imgPath);
                }

                cx.Users.Add(UserObject);
                cx.SaveChanges();
                TempData["LoginMessage"] = "You have been registered successfully.";
                return RedirectToAction("Index");
            }
            TempData["LoginMessage"] = "Email or Student id are repeated.";

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ForgotPassword(User usrObject)
        {
            User usr = cx.Users.FirstOrDefault(x => x.mail == usrObject.mail);
            if (usr != null)
            {
                emailSending emailObject = new emailSending(usr.mail);

                emailObject.ForgotPass(usr.pass);
                TempData["LoginMessage"] = "An email has been sent to you. Please check you mail box";

            }
            else
            {
                TempData["LoginMessage"] = "This email is not registered in our database";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}
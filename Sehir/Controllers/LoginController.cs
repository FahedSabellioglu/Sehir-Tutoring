using System.Web.Mvc;

namespace Sehir.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    public class LoginController : Controller
    {
        // GET: Login
        SehirEntities cx = new SehirEntities();
        public ActionResult Index()
        {
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace Sehir.Controllers
{
    [Authorize]
    public class HomeworkController : Controller
    {
        // GET: Homework
        SehirEntities ctx = new SehirEntities();
        public ActionResult Index(int? page, int? id=0 )
        {

            List<HomworkMaker> HomeworkList = Data(id);
            PagedList<HomworkMaker> j = new PagedList<HomworkMaker>(HomeworkList, page ?? 1, 9);
            ViewBag.HomeworkNames = HomeworkList.OrderBy(x => x.H_Name).Select(x => x.H_Name).Distinct().ToList();

            List<string> Deps = ctx.Homework.OrderBy(x => x.dept).Select(x => x.dept).Distinct().ToList();
            ViewBag.dept_Names = Deps;

            ViewBag.name = "Homeworks";
            ViewBag.desc = "Homework desc";
            ViewBag.A_name = "Index";
            ViewBag.C_name = "Homework";

            return View(j);
        }

        List<HomworkMaker> Data(int? id=1)
        {
            List<HomworkMaker> HomeworkList;
            User usr = ctx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);
            if (id == 1)
            {
                HomeworkList = ctx.HomeworkList(usr.id, true).ToList();
            }

            else
            {
                HomeworkList = ctx.HomeworkList(usr.id, false).ToList();
            }
            return HomeworkList;

        }



        [HttpPost]
        public void Delete(int userId, string name, string code)
        {
            HomworkMaker homeworkObject = ctx.HomworkMakers.FirstOrDefault(x => x.ID == userId && x.H_Name == name && x.H_Code == code);

            ctx.HomworkMakers.Remove(homeworkObject);
            ctx.SaveChanges();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Sehir.Controllers
{
        
    public class CoursesController : Controller
    {
        
          // GET: Courses
        SehirEntities cx = new SehirEntities();
        public ActionResult Index(int? page, int? id=0)
        {
            List<CoursesList_Result> courses_offered = Data(id);
            List<string> Allcourses = courses_offered.OrderBy(x=>x.C_Name).Select(x => x.C_Name).Distinct().ToList();
            List<string> Deps = cx.Courses.Select(x => x.dept).Distinct().ToList();
            PagedList<CoursesList_Result> j = new PagedList<CoursesList_Result>(courses_offered, page ?? 1, 9);


            ViewBag.name = "Courses";
            ViewBag.desc = "Course desc";
            ViewBag.A_name = "Index";
            ViewBag.C_name = "Courses";


            ViewBag.CNames = Allcourses;
            ViewBag.Dep = Deps;

            return View(j);
        }

        List<CoursesList_Result> Data(int? id)
        {
            List<CoursesList_Result> CoursesList;
            User usr = cx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);
            if (id == 1)
            {
                CoursesList = cx.CoursesList(usr.id, true).ToList();
                
            }
            else
            {
                CoursesList = cx.CoursesList(usr.id, false).ToList();
            }
            return CoursesList;
        }

        public ActionResult Details(CoursesList_Result CourseObject)
        {
            User usr = cx.Users.FirstOrDefault(x => x.id == CourseObject.ID);
            ViewData["UserInfo"] = usr;

            ViewBag.UserInfo = usr;
            
            return View(CourseObject);
        }

        public ActionResult UserId(string Email, int crid)
        {
            User usr = cx.Users.FirstOrDefault(x => x.mail == Email);
            return RedirectToAction("Details", new { usrid = usr.id, crid = crid });
        }   

 
        [HttpPost]
        public void Delete(int userId, string name, string code)
        {
            User usr = cx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);
            Lecturer lec_index = cx.Lecturers.FirstOrDefault(x => x.ID == usr.id && x.C_Name == name && x.C_Code==code);

            cx.Lecturers.Remove(lec_index);
            cx.SaveChanges();

            
        }

    }
}
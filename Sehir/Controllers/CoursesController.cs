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
    [Authorize]
    public class CoursesController : Controller
    {
        SehirTutoringEntities cx = new SehirTutoringEntities();
        public ActionResult Index(int? page, int? id=0)
        {
            List<CoursesList_Result> courses_offered = Data(id);
            List<string> Allcourses = courses_offered.Select(x => x.CName).Distinct().ToList();
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
 

        public ActionResult Details(Lecturer CourseObject)
        {
            UserCourseInfo_Result usrCoruse = cx.UserCourseInfo(CourseObject.ID, CourseObject.C_Code).First();


            User UsrObject = cx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);
            ViewBag.check = CourseObject.ID != UsrObject.id ? true : false;


            ViewData["UserObject"] = UsrObject;
            return View("~/Views/Shared/Details.cshtml",usrCoruse);
            
        }

        public ActionResult UserId(string Email, int crid)
        {
            User usr = cx.Users.FirstOrDefault(x => x.mail == Email);
            return RedirectToAction("Details", new { usrid = usr.id, crid = crid });
        }   

 
        [HttpPost]
        [Authorize(Roles ="Admin,Lecturer")]
        public void Delete(int userId, string name, string code)
        {
            User usr = cx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);
            Lecturer lec_index = cx.Lecturers.FirstOrDefault(x => x.ID == usr.id  && x.C_Code==code);

            cx.Lecturers.Remove(lec_index);
            cx.SaveChanges();
        }

        public ActionResult Comment(C_feedBack feedbackObject)
        {
            feedbackObject.S_ID = cx.Users.FirstOrDefault(x => x.mail == User.Identity.Name).id;
            feedbackObject.reviewDate = DateTime.Now;
            cx.C_feedBack.Add(feedbackObject);
            cx.SaveChanges();

            return getFeedbacks(feedbackObject);
        }

        public ActionResult getFeedbacks (C_feedBack FeedBackObject)
        {
            List<CoursesFeedBacks_Result> feedbacks = cx.CoursesFeedBacks(FeedBackObject.T_ID, FeedBackObject.C_Code).ToList();
            User UsrObject = cx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);

            ViewBag.check = FeedBackObject.T_ID != UsrObject.id ? true : false;


            return PartialView("~/Views/Shared/Feedback.cshtml", feedbacks);

        }
        [HttpPost]
        public void C_Req(CourseRequest Req)
        {
            Req.ReqDate = DateTime.Now;

            cx.CourseRequests.Add(Req);
            cx.SaveChanges();
        }
    }
}
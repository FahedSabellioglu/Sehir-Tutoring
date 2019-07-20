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
        SehirTutoringEntities ctx = new SehirTutoringEntities();
        public ActionResult Index(int? page, int? id = 0)
        {

            List<HomeworkList_Result> HomeworkList = Data(id);
            PagedList<HomeworkList_Result> j = new PagedList<HomeworkList_Result>(HomeworkList, page ?? 1, 9);
            ViewBag.HomeworkNames = HomeworkList.OrderBy(x => x.title).Select(x => x.title).Distinct().ToList();

            List<string> Deps = ctx.Courses.OrderBy(x => x.dept).Select(x => x.dept).Distinct().ToList();
            ViewBag.dept_Names = Deps;

            ViewBag.name = "Homeworks";
            ViewBag.desc = "Homework desc";
            ViewBag.A_name = "Index";
            ViewBag.C_name = "Homework";

            return View(j);
        }

        List<HomeworkList_Result> Data(int? id = 1)
        {
            List<HomeworkList_Result> HomeworkList;
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

        public ActionResult Details(HomeworkMaker HWMaker)
        {
            UserHomeworkInfo_Result Homeworkdata = ctx.UserHomeworkInfo(HWMaker.ID, HWMaker.title, HWMaker.C_Code).First();

            UserAuth(HWMaker.ID);

            return View("~/Views/Shared/Details.cshtml", Homeworkdata);
        }

        void UserAuth(int id)
        {
            User UsrObject = ctx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);
            ViewData["UserObject"] = UsrObject;
            ViewBag.check = id != UsrObject.id ? true : false;

        }

        public ActionResult getFeedbacks(H_feedBack HWObject)
        {
            List<HomeworkFeedBacks_Result> feedbacks = ctx.HomeworkFeedBacks(HWObject.H_ID, HWObject.C_Code, HWObject.title).ToList();


            User UsrObject = ctx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);

            ViewBag.check = HWObject.H_ID != UsrObject.id ? true : false;


            return PartialView("~/Views/Shared/Feedback.cshtml", feedbacks);
        }

        public ActionResult Comment(H_feedBack feedbackObject)
        {
            feedbackObject.S_ID = ctx.Users.FirstOrDefault(x => x.mail == User.Identity.Name).id;
            feedbackObject.reviewDate = DateTime.Now;
            ctx.H_feedBack.Add(feedbackObject);
            ctx.SaveChanges();

            return getFeedbacks(feedbackObject);
        }


        [HttpPost]
        public void Delete(int userId, string name, string code)
        {
            HomeworkMaker homeworkObject = ctx.HomeworkMakers.FirstOrDefault(x => x.ID == userId && x.C_Code == code);

            ctx.HomeworkMakers.Remove(homeworkObject);
            ctx.SaveChanges();

        }

        [HttpPost]
        public void H_Req(HomeworkRequest Req)
        {
            Req.ReqDate = DateTime.Now;

            ctx.HomeworkRequests.Add(Req);
            ctx.SaveChanges();
        }
 

    }
}
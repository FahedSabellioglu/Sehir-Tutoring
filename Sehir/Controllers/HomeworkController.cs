using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Sehir.App_Classes;

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

            User usrObjectLec = ctx.Users.FirstOrDefault(x => x.id == Req.H_ID);
            User usrObjectStu = ctx.Users.FirstOrDefault(x => x.id == Req.S_ID);

            emailSending mailObject = new emailSending(usrObjectLec.mail,usrObjectStu.mail);

            mailObject.SendEmail(Req.C_Code,Req.title,Req.note);

            ctx.HomeworkRequests.Add(Req);
            ctx.SaveChanges();
        }

        public ActionResult HomeworkOfferPage(string mail)
        {
            if (TempData["shortMessage"] != null)
            {
                ViewBag.message = TempData["shortMessage"].ToString();
            }

            User ursObject = ctx.Users.FirstOrDefault(x => x.mail == mail);
            List<CoursesOffer_Result> CoursesData = ctx.CoursesOffer(ursObject.id).ToList();

            if (ursObject.transcript == null)
            {
                ViewBag.transcript = true;
            }

            ViewBag.Hdata = ctx.HomeworkList(ursObject.id, false).ToList();

            return View("~/Views/Shared/Offer.cshtml", CoursesData);

        }

        byte[] ChangeImgeToArray(HttpPostedFileBase file)
        {
            BinaryReader rdr = new BinaryReader(file.InputStream);
            return rdr.ReadBytes((int)file.ContentLength);
        }

        [HttpPost]
        public ActionResult Homework_Req(string C_Code,string title, string C_Name, string dept, string descrip, int price, HttpPostedFileBase img, HttpPostedFileBase transcript)
        {
            try
            {
                byte[] imageByte = img != null ? ChangeImgeToArray(img) : null;

                Homework HomeworkObject = new Homework();


                User usrObejct = ctx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);
                if (transcript != null)
                {
                    byte[] TranscriptByte = ChangeImgeToArray(transcript);
                    usrObejct.transcript = TranscriptByte;
                }



                if (ctx.homework_request.Any(x=>x.ID == usrObejct.id && x.C_Code == C_Code && x.title == title ))
                {
                    TempData["shortMessage"] = "You have already sent a request, Please be patient.";

                    return RedirectToAction("HomeworkOfferPage", new { mail = User.Identity.Name });

                }

                if (C_Name != C_Code)
                {
                    Course CourseObject = new Course();
                    CourseObject.C_Code = C_Code;
                    CourseObject.C_Name = C_Name;
                    CourseObject.dept = dept;

                    ctx.Courses.Add(CourseObject);
                    ctx.SaveChanges();

                }
                if (!ctx.Homework.Any(x=>x.title == title && x.C_Code == C_Code))
                {
                    HomeworkObject.title = title;
                    HomeworkObject.C_Code = C_Code;
                    ctx.Homework.Add(HomeworkObject);
                    ctx.SaveChanges();

                }
                

                homework_request HWObject = new homework_request();

                HWObject.C_Code = C_Code;
                HWObject.descrip = descrip;
                HWObject.ID = usrObejct.id;
                HWObject.img = imageByte;
                HWObject.title = title;
                HWObject.price = price;



                ctx.homework_request.Add(HWObject);
                ctx.SaveChanges();



                TempData["shortMessage"] = "Your request has been sent, the admins will check and get back to you.";

            }
            catch 
            {
                TempData["shortMessage"] = "Your request has not been sent, Please try again later";
            }

            return RedirectToAction("HomeworkOfferPage", new { mail = User.Identity.Name });

        }


    }
}
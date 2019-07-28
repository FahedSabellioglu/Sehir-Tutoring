using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Drawing;
using Sehir.App_Classes;

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
            PagedList<CoursesList_Result> coursesPaged = new PagedList<CoursesList_Result>(courses_offered, page ?? 1, 9);

            ViewBag.CNames = Allcourses;
            ViewBag.Dep = Deps;

            return View(coursesPaged);
        }

        // Show the offer page
        public ActionResult CourseOfferPage(string mail)
        {
            if (TempData["shortMessage"] != null)
            {
                ViewBag.message = TempData["shortMessage"].ToString();
            }
           
            User usrObject = cx.Users.FirstOrDefault(x => x.mail == mail);

            if (usrObject.transcript == null)
            {
                ViewBag.Transcript = true;
            }

            List<CoursesOffer_Result> CoursesData = cx.CoursesOffer(usrObject.id).ToList();

            return View("~/Views/Shared/Offer.cshtml", CoursesData);
            
        }

        byte[] ChangeImgeToArray(HttpPostedFileBase file)
        {
            BinaryReader rdr = new BinaryReader(file.InputStream);
            return rdr.ReadBytes((int)file.ContentLength);
        }

        // submit the offer
        [HttpPost]
        public ActionResult Course_Req( string C_Code,string C_Name,string dept, string descrip, int price,HttpPostedFileBase img, HttpPostedFileBase transcript)
        {
            try
            {
                byte[] imageByte = img != null? ChangeImgeToArray(img): null;

                User usrObject = cx.Users.FirstOrDefault(x => x.mail == User.Identity.Name);

                if (cx.lec_request.Any(x=>x.ID==usrObject.id && x.C_Code==C_Code )) // in case already has a request
                {
                    TempData["shortMessage"] = "You have already sent a request, Please be patient.";
                    return RedirectToAction("CourseOfferPage", new { mail = User.Identity.Name });

                }

                if (usrObject.transcript==null)
                {
                    if(transcript == null)
                    {
                        TempData["shortMessage"] = "Please upload your transcript.";
                        return RedirectToAction("CourseOfferPage", new { mail = User.Identity.Name });
                    }
                    byte[] TranscriptImg = ChangeImgeToArray(transcript);
                    usrObject.transcript = TranscriptImg;
                }

                if (C_Name != C_Code) // a new course will be added if this case is true
                {
                    Course CourseObject = new Course();
                    CourseObject.C_Code = C_Code;
                    CourseObject.C_Name = C_Name;
                    CourseObject.dept = dept;
                    cx.Courses.Add(CourseObject);
                }

                lec_request LecObject = new lec_request();
                LecObject.ID = usrObject.id;
                LecObject.C_Code = C_Code;
                LecObject.img = imageByte;
                LecObject.price = price;
                LecObject.descrip = descrip;
                cx.lec_request.Add(LecObject);

                cx.SaveChanges();
                TempData["shortMessage"] = "Your request has been sent, the admins will check and get back to you.";

            }
            catch
            {
                TempData["shortMessage"] = "Your request has not been sent, Please try again later";
            }

            return RedirectToAction("CourseOfferPage",new {mail=User.Identity.Name });

        }

        List<CoursesList_Result> Data(int? id) // get the data to be shown in the index page
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
 
        [HttpPost]
        [Authorize(Roles = "Admin,Lecturer,HomeworkMaker")]
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

            User usrObjectLec = cx.Users.FirstOrDefault(x => x.id == Req.T_ID);
            User usrObjectStu = cx.Users.FirstOrDefault(x => x.id == Req.S_ID);

            emailSending mailObject = new emailSending(usrObjectLec.mail, usrObjectStu.mail);
            string title = cx.Courses.FirstOrDefault(x => x.C_Code == Req.C_Code).C_Name;

            mailObject.SendEmail(Req.C_Code,title, Req.note);


            cx.CourseRequests.Add(Req);

            cx.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace Sehir.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        SehirTutoringEntities cx = new SehirTutoringEntities();
        public ActionResult PendingCourses(int? page) // the pending courses to be approved by the admin.
        {
            List<CourseRequestsPending_Result> Requests = cx.CourseRequestsPending(null, null).ToList();
            List<string> Names = Requests.Select(x => x.C_Name).Distinct().ToList();
            List<string> Deps = cx.Courses.Select(x => x.dept).Distinct().ToList();
            PagedList<CourseRequestsPending_Result> Pages = new PagedList<CourseRequestsPending_Result>(Requests, page ?? 1, 9);

            if (TempData["Message"] != null)
            {
                ViewBag.message = TempData["Message"].ToString();
            }


            ViewBag.Deps = Deps;
            ViewBag.Names = Names;
            return View(Pages);
        }

        public ActionResult HomeworksPending(int? page) // Homeworks pending to be approved by the admin.
        {
            List<HomeworksRequestsPending_Result> Requests = cx.HomeworksRequestsPending(null,null,null).ToList();
            List<string> Names = Requests.OrderBy(x => x.C_Name).Select(x => x.C_Name).Distinct().ToList();
            List<string> Deps = cx.Courses.Select(x => x.dept).Distinct().ToList();

            PagedList<HomeworksRequestsPending_Result> Pages = new PagedList<HomeworksRequestsPending_Result>(Requests, page ?? 1, 9);

            ViewBag.Deps = Deps;
            ViewBag.Names = Names;

            if (TempData["Message"] != null)
            {
                ViewBag.message = TempData["Message"].ToString();
            }


            return View(Pages);
        }



        public ActionResult ItemDetails(int ID, string C_Code,string title="")
        {
            if (title!="")
            {
                ViewBag.ActionName = "Homework";

                HomeworksRequestsPending_Result Hwdetails = cx.HomeworksRequestsPending(ID, C_Code, title).First();
                return View("~/Views/Shared/RequestsDetails.cshtml", Hwdetails);

            }

            ViewBag.ActionName = "Courses";

            CourseRequestsPending_Result CourseDetails = cx.CourseRequestsPending(ID, C_Code).First();
            return View("~/Views/Shared/RequestsDetails.cshtml", CourseDetails);

        }


        
        public ActionResult Downlaod(int ID)
        {
            byte[] ImgFile = cx.Users.FirstOrDefault(x => x.id == ID).img;
            return File(ImgFile, System.Net.Mime.MediaTypeNames.Application.Octet, "Transcript_" + ID+".png");
        }



    
        public ActionResult CourseApproval(int ID, string C_Code)
        {
            lec_request LecReqObject = cx.lec_request.FirstOrDefault(x => x.ID == ID && x.C_Code == C_Code);
            Lecturer LecObject = new Lecturer();
            LecObject.C_Code = LecReqObject.C_Code;
            LecObject.descrip = LecReqObject.descrip;
            LecObject.ID = LecReqObject.ID;
            LecObject.Price = LecReqObject.price;
            LecObject.img = LecReqObject.img;

            try
            {
                cx.lec_request.Remove(LecReqObject);
                cx.Lecturers.Add(LecObject);
                cx.SaveChanges();

                TempData["Message"] = "Changes has been saved";
            }
            catch
            {
                TempData["Message"] = "Failed, Try again alter";
            }

            
            return RedirectToAction("PendingRequest");
        }


       
        public ActionResult CourseDeletion(int ID, string C_Code)
        {
            lec_request LecReqObject = cx.lec_request.FirstOrDefault(x => x.ID == ID && x.C_Code == C_Code);
            try
            {
                cx.lec_request.Remove(LecReqObject);
                cx.SaveChanges();
                TempData["Message"] = "Item has been deleted";
            }
            catch
            {
                TempData["Message"] = "Failed to delete the item";
            }

            return RedirectToAction("PendingRequest");
        }


        
        public ActionResult HomeworkApproval(int ID, string C_Code, string title)
        {
            homework_request HwReqObject = cx.homework_request.FirstOrDefault(x => x.ID == ID && x.title == title && x.C_Code == C_Code);

            HomeworkMaker HwObject = new HomeworkMaker();

            HwObject.C_Code = HwReqObject.C_Code;
            HwObject.img = HwReqObject.img;
            HwObject.price = HwReqObject.price;
            HwObject.ID = HwReqObject.ID;
            HwObject.descrip = HwReqObject.descrip;
            HwObject.title = title;

            try
            {
                cx.homework_request.Remove(HwReqObject);
                cx.HomeworkMakers.Add(HwObject);
                cx.SaveChanges();
                TempData["Message"] = "Changed has been saved.";

            }
            catch
            {
                TempData["Message"] = "Failed, Try again alter";

            }

            return RedirectToAction("HomeworksPending");

        }


        public ActionResult HwDeletion(int ID, string C_Code, string title)
        {
            homework_request HwReqObject = cx.homework_request.FirstOrDefault(x => x.ID == ID && x.title == title && x.C_Code == C_Code);

            try
            {
                cx.homework_request.Remove(HwReqObject);
                cx.SaveChanges();
                TempData["Message"] = "Item has been deleted";

            }
            catch
            {
                TempData["Message"] = "Failed to delete the item";

            }

            return RedirectToAction("HomeworksPending");


        }

    }
}
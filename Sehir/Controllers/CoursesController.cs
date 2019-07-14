﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Sehir.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index(int? page)
        {
            List<string> CoursesNames = new List<string>() { "Introduction to programming", "Network", "Computer Network" };
            List<string> Dept_names = new List<string>() { "CS", "IEEE", "IMB" };

            List<string> hnames = new List<string>() { "Introduction to programming", "Network", "Computer Network", "Introduction to programming", "Network", "Computer Network", "Introduction to programming", "Network", "Computer Network",
            "Introduction to programming 1", "1 Network", "2 Computer Network", "1 Introduction to programming", "2 Network", "3 Computer Network", "4 Introduction to programming", "Network", "Computer Network" };

            List<string> h = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
            PagedList<string> j = new PagedList<string>(h, page ?? 1, 9);

            ViewBag.dept = Dept_names;
            ViewBag.h = hnames;
            ViewBag.object_name = CoursesNames;

            ViewBag.name = "Courses";
            ViewBag.desc = "Course desc";
            ViewBag.A_name = "Index";
            ViewBag.C_name = "Courses";



            return View(j);
        }
  
        public ActionResult Details(int id)
        {
            string name;
            if (id == 1)
            {
                name = "h";
            }
            else
            {
                name = "j";
            }
            ViewBag.page_id = id;
                
            ViewBag.c_name = name;
            return View();
        }
        public ActionResult Try(int? page)
        {
            List<string> hnames = new List<string>() { "Introduction to programming", "Network", "Computer Network", "Introduction to programming", "Network", "Computer Network", "Introduction to programming", "Network", "Computer Network" };

            return View(hnames.ToPagedList(page?? 1, 2));
        }
        [HttpPost]
        public string Delete(int id)
        {
            string n = "";

            int d = id;
            if (d == 1)
            {
                n = "done";
                return n;
            }
            else
            {
                n = "fail";
                return n;
            }
        }

    }
}
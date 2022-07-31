using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skilldemy.Models;

namespace Skilldemy.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            List<Object> model = new List<Object>();
            List<Course> courses = _context.Courses.ToList();
            List<Image> images = _context.Images.ToList();
            model.Add(courses);
            model.Add(images);

            return View(model);
        }

        public ActionResult ShowCourse(int id)
        {
            if (id != null && id != 0)
            {
                Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
                return View("Course", course);
            }
            else
                return View();
            
        }

        /*
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        */
    }
}
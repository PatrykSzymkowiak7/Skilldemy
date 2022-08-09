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

        public ActionResult SearchByText(string searchPhrase)
        {
            List<Object> model = new List<Object>();

            List<Category> categories = _context.Categories.ToList();
            var category = categories.FirstOrDefault(c => c.Name.Contains(searchPhrase) == true);

            List<Course> courses = _context.Courses.Where(c => c.CategoryId == category.Id
            || c.Title.Contains(searchPhrase) == true
            || c.Description.Contains(searchPhrase) == true)
                .ToList();

            List<Image> allImages = _context.Images.ToList();
            List<Image> images = new List<Image>();
            foreach(var img in allImages)
            {
                if(courses.Any(c => c.Id == img.Id))
                {
                    images.Add(img);
                }
            }

            model.Add(courses);
            model.Add(images);

            return View("Index", model);
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
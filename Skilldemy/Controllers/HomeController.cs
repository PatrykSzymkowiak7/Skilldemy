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
        public List<Course> InitCourses()
        {
            Tag tag1 = new Tag
            {
                Id = 1,
                Name = "Gastronomia"
            };

            Tag tag2 = new Tag
            {
                Id = 2,
                Name = "Kuchnia",
            };

            Tag tag3 = new Tag
            {
                Id = 3,
                Name = "Kulinaria"
            };

            List<Tag> tags = new List<Tag>();

            tags.Add(tag1);
            tags.Add(tag2);
            tags.Add(tag3);

            Course course = new Course
            {
                Id = 1,
                Title = "To jest przykładowy tytuł",
                Description = "To jest przykładowy opis kursu",
                OwnerId = 1,
                //Tags = tags,
                Price = 49.99,
                Discount = 0,
                Rating = 5.00,
                Reviews = 17,
                Difficulty = "Początkujący"
            };

            List<Course> courses = new List<Course>();
            courses.Add(course);

            return courses;
        }

        public ActionResult Index()
        {
            return View(InitCourses());
        }

        public ActionResult Course()
        {
            return View(InitCourses());
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
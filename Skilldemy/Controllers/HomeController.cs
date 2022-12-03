using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            List<Course> courses = new List<Course>(_context.Courses.Where(c => c.IsVisible == true)).ToList();
            List<Image> images = _context.Images.ToList();

            model.Add(courses);
            model.Add(images);

            return View(model);
        }

        public ActionResult SearchByText(string searchPhrase)
        {
            _context.Database.Connection.Open();
            List<Object> model = new List<Object>();
            List<Category> categories = _context.Categories.ToList();
            List<Course> courses = new List<Course>();
            List<Course> coursesFiltered = new List<Course>();
            Category category = new Category();
            
            if(searchPhrase != "")
            {
                category = categories.FirstOrDefault(c => c.Name.Contains(searchPhrase) == true);
            }

            if(category != null)
            {
                courses = _context.Courses.Where(c => c.CategoryId == category.Id
                || (c.Title.Contains(searchPhrase) == true)
                || (c.Description.Contains(searchPhrase) == true)
                && (c.IsVisible == true))
                    .ToList();

                foreach (var course in courses)
                {
                    if (course.IsVisible)
                    {
                        coursesFiltered.Add(course);
                    }
                }
            }
            else
            {
                courses = _context.Courses.Where(c => c.Title.Contains(searchPhrase) == true
                || (c.Description.Contains(searchPhrase) == true)
                && (c.IsVisible == true))
                .ToList();

                foreach (var course in courses)
                {
                    if (course.IsVisible)
                    {
                        coursesFiltered.Add(course);
                    }
                }
            }

            List<Image> allImages = _context.Images.ToList();

            List<Image> images = new List<Image>();
            foreach(var img in allImages)
            {
                if(coursesFiltered.Any(c => c.Id == img.Id))
                {
                    images.Add(img);
                }
            }

            model.Add(coursesFiltered);
            model.Add(images);

            _context.Database.Connection.Close();
            return View("Index", model);
        }

        public ActionResult SortCourses(string sortBy)
        {
            List<Object> model = new List<Object>();
            List<Course> courses = _context.Courses.Where(c => c.IsVisible == true).ToList();
            List<Course> coursesFiltered = new List<Course>();
            List<Image> allImages = _context.Images.ToList();
            List<UUIDConnection> uuidConnections = _context.UUIDConnections.ToList();

            if (sortBy == "Newest")
            {
                courses.OrderByDescending(c => c.CreatedDate);
                model.Add(courses);
                model.Add(allImages);

                return View("Index", model);
            }

            if(sortBy == "Popularity")
            {
                var sortedConnections = uuidConnections.GroupBy(x => x.CourseId)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g).ToList();

                for(int i = 0; i<sortedConnections.Count; i++)
                {
                    foreach(var course in courses)
                    {
                        if(course.Id == sortedConnections[i].Key)
                        {
                            coursesFiltered.Add(course);
                        }
                    }
                }

                List<Image> images = new List<Image>();
                foreach (var img in allImages)
                {
                    if (coursesFiltered.Any(c => c.Id == img.Id))
                    {
                        images.Add(img);
                    }
                }

                model.Add(coursesFiltered);
                model.Add(images);

                return View("Index", model);
            }

            model.Add(courses);
            model.Add(allImages);

            return View("Index", model);
        }
    }
}
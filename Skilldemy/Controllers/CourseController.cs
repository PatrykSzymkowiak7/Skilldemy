using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skilldemy.Models;

namespace Skilldemy.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;

        CourseController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
    }
}
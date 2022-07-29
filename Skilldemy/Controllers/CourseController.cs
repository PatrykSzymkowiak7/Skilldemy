using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Skilldemy.Models;

namespace Skilldemy.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;

        public CourseController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        // GET: Create course view
        public ActionResult CreateCourse() 
        {
            var createCourseViewModel = new CreateCourseViewModel();
            return View(createCourseViewModel);
        }

        // POST: New course
        [HttpPost]
        public ActionResult CreateCourse(CreateCourseViewModel createCourseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createCourseViewModel);
            }

            Course course = new Course();

            var currentUserId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == currentUserId);

            course.CreatedDate = DateTime.Now;
            course.OwnerId = currentUserId;
            course.OwnerUserName = user.UserName;
            course.Price = createCourseViewModel.Price;
            course.Title = createCourseViewModel.Title;
            course.Description = createCourseViewModel.Description;
            var highestIdCourse = _context.Courses.OrderByDescending(c => c.Id).FirstOrDefault();

            if (highestIdCourse == null)
            {
                course.Id = 1;
            }
            else
            {
                course.Id = highestIdCourse.Id + 1;
            }

            // Image serialization to DB
            if (createCourseViewModel.ImageFile != null)
            {
                Image image = new Image();

                byte[] uploadedImage = new byte[createCourseViewModel.ImageFile.InputStream.Length];
                createCourseViewModel.ImageFile.InputStream.Read(uploadedImage, 0, uploadedImage.Length);

                image.ImageFile = uploadedImage;
                image.CourseId = course.Id;
                image.OwnerId = course.OwnerId;
                image.UploadedDate = DateTime.Now;

                _context.Images.Add(image);
            }



            /*// Video serialization to DB
            byte[] uploadedVideo = new byte[createCourseViewModel.VideoFile.InputStream.Length];
            createCourseViewModel.VideoFile.InputStream.Read(uploadedVideo, 0, uploadedVideo.Length);
            */

            
            _context.Courses.Add(course);
            _context.SaveChanges();

            return Content("Kurs utworzony pomyślnie");
        }
        
        // GET: Edit course view
        public ActionResult EditCourse()
        {
            return View();
        }

        // POST: Edited course
        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult ShowImage(int id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            if(image != null)
            {
                if(image.ImageFile != null)
                {
                    return File(image.ImageFile, "image/jpg");
                }
            }

            return null;
        }
    }
}
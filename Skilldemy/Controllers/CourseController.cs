using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            course.CategoryId = createCourseViewModel.CategoryId;
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
            // Need to delay taking pictures from the database because it throws
            // "The underlying provider failed on Open" if it happens too fast
            // Another fix could be MultipleActiveResultSets=True in connection string but it is not supported 
            var task = Task.Delay(1000).ContinueWith(t => Console.WriteLine(DateTime.Now));
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            task.Wait();

            if (image != null)
            {
                if(image.ImageFile != null)
                {
                    return File(image.ImageFile, "image/jpg");
                }
            }

            return null;
        }

        public ActionResult ShowVideo(int id)
        {
            // Need to delay taking videos from the database because it throws
            // "The underlying provider failed on Open" if it happens too fast
            // Another fix could be MultipleActiveResultSets=True in connection string but it is not supported 
            var task = Task.Delay(1000).ContinueWith(t => Console.WriteLine(DateTime.Now));
            var video = _context.Videos.FirstOrDefault(i => i.Id == id);
            task.Wait();

            if (video != null)
            {
                if (video.VideoFile != null)
                {
                    return File(video.VideoFile, "video/mp4");
                }
            }

            return null;
        }
    }
}
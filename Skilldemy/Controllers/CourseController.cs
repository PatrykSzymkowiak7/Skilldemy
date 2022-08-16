using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Skilldemy.Models;
using Skilldemy.Helpers;

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

            // Video serialization to DB
            if (createCourseViewModel.PreviewVideoFile != null)
            {
                Video previewVideo = new Video();

                byte[] uploadedVideo = new byte[createCourseViewModel.PreviewVideoFile.InputStream.Length];
                createCourseViewModel.PreviewVideoFile.InputStream.Read(uploadedVideo, 0, uploadedVideo.Length);

                previewVideo.VideoFile = uploadedVideo;
                previewVideo.IsPreviewVideo = true;
                previewVideo.OwnerId = course.OwnerId;
                previewVideo.CourseId = course.Id;

                _context.Videos.Add(previewVideo);
            }

            SectionViewModel sectionViewModel = new SectionViewModel();
            sectionViewModel.CourseId = course.Id;

            _context.Courses.Add(course);
            _context.SaveChanges();

            return View("ManageSections", sectionViewModel);
        }

        public ActionResult ShowImage(int id)
        {
            // Need to delay getting pictures from the database because it throws
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
            // Need to delay getting videos from the database because it throws
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

        public ActionResult AddSection(SectionViewModel sectionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(sectionViewModel);
            }

            var currentUserId = User.Identity.GetUserId();
            Video video = new Video();

            video.CourseId = sectionViewModel.CourseId;
            video.IsPreviewVideo = false;
            video.OwnerId = sectionViewModel.OwnerId;
            video.SectionDescription = sectionViewModel.SectionDescription;
            video.SectionTitle = sectionViewModel.SectionTitle;
            video.UploadedDate = DateTime.Now;
            video.OwnerId = currentUserId;

            // Video serialization to DB
            if (sectionViewModel.VideoFile != null)
            {
                byte[] uploadedVideo = new byte[sectionViewModel.VideoFile.InputStream.Length];
                sectionViewModel.VideoFile.InputStream.Read(uploadedVideo, 0, uploadedVideo.Length);

                video.VideoFile = uploadedVideo;
            }

            _context.Videos.Add(video);
            _context.SaveChanges();

            SectionViewModel nextSectionViewModel = new SectionViewModel();
            nextSectionViewModel.CourseId = sectionViewModel.CourseId;

            return View("SectionCreated", nextSectionViewModel);
        }

        public ActionResult AddAnotherSection(int id)
        {
            SectionViewModel sectionViewModel = new SectionViewModel();
            sectionViewModel.CourseId = id;

            return View("ManageSections", sectionViewModel);
        }

        public ActionResult CoursePreview(int id)
        {
            CoursePreviewViewModel coursePreviewViewModel = new CoursePreviewViewModel();
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            List<Video> videos = (_context.Videos.Where(v => v.CourseId == id)).ToList();

            coursePreviewViewModel.Course = course;
            coursePreviewViewModel.Videos = videos;

            return View("CoursePreview", coursePreviewViewModel);
        }

        public ActionResult MyCourses()
        {
            var currentUserId = User.Identity.GetUserId();
            List<Object> model = new List<Object>();
            List<Course> courses = new List<Course>(_context.Courses.Where(c => c.OwnerId == currentUserId));
            List<Image> images = _context.Images.ToList();

            model.Add(courses);
            model.Add(images);

            return View("MyCourses", model);
        }

        public ActionResult SetVisibility(int id)
        {
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if(course.IsVisible)
            {
                course.IsVisible = false;
            }
            else 
            {
                course.IsVisible = true;
            }

            _context.SaveChanges();

            return RedirectToAction("MyCourses");
        }

        public ActionResult DeleteCourse(int id)
        {
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            _context.Courses.Remove(course);

            return RedirectToAction("MyCourses");
        }

        public ActionResult EditCourseRedirect(int id)
        {
            EditCourseViewModel editCourseViewModel = new EditCourseViewModel();
            Course course = new Course();
            Image image = new Image();
            image = _context.Images.FirstOrDefault(i => i.CourseId == id);

            Video video = new Video();
            video = _context.Videos.FirstOrDefault(v => v.CourseId == id && v.IsPreviewVideo == true);

            course = _context.Courses.FirstOrDefault(c => c.Id == id);

            editCourseViewModel.Id = course.Id;
            editCourseViewModel.CategoryId = course.CategoryId;
            editCourseViewModel.Description = course.Description;
            editCourseViewModel.OwnerId = course.OwnerId;
            editCourseViewModel.Price = course.Price;
            editCourseViewModel.Title = course.Title;
            editCourseViewModel.PreviewVideoFile = new HttpPostedFileBaseHelper(video.VideoFile, "Preview video" + course.Id);
            editCourseViewModel.ImageFile = new HttpPostedFileBaseHelper(image.ImageFile, "Preview image" + course.Id);

            return View("EditCourse", editCourseViewModel);
        }

        public ActionResult EditCourse(EditCourseViewModel editCourseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editCourseViewModel);
            }

            Course course = _context.Courses.FirstOrDefault(c => c.Id == editCourseViewModel.Id);

            var currentUserId = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == currentUserId);

            course.OwnerId = currentUserId;
            course.OwnerUserName = user.UserName;
            course.Price = editCourseViewModel.Price;
            course.Title = editCourseViewModel.Title;
            course.Description = editCourseViewModel.Description;
            course.CategoryId = editCourseViewModel.CategoryId;

            // Image serialization to DB
            if (editCourseViewModel.ImageFile != null)
            {
                Image image = new Image();
                image = _context.Images.FirstOrDefault(i => i.CourseId == image.CourseId);

                byte[] uploadedImage = new byte[editCourseViewModel.ImageFile.InputStream.Length];
                editCourseViewModel.ImageFile.InputStream.Read(uploadedImage, 0, uploadedImage.Length);

                image.ImageFile = uploadedImage;
                image.UploadedDate = DateTime.Now;
            }

            // Video serialization to DB
            if (editCourseViewModel.PreviewVideoFile != null)
            {
                Video previewVideo = new Video();
                previewVideo = _context.Videos.FirstOrDefault(v => v.CourseId == previewVideo.CourseId);

                byte[] uploadedVideo = new byte[editCourseViewModel.PreviewVideoFile.InputStream.Length];
                editCourseViewModel.PreviewVideoFile.InputStream.Read(uploadedVideo, 0, uploadedVideo.Length);

                previewVideo.VideoFile = uploadedVideo;
            }

            _context.SaveChanges();
            return RedirectToAction("MyCourses");
        }

        public ActionResult ShowImageEdit(int id)
        {
            // Need to delay getting pictures from the database because it throws
            // "The underlying provider failed on Open" if it happens too fast
            // Another fix could be MultipleActiveResultSets=True in connection string but it is not supported 
            var task = Task.Delay(1000).ContinueWith(t => Console.WriteLine(DateTime.Now));
            var image = _context.Images.FirstOrDefault(i => i.CourseId == id);
            task.Wait();

            if (image != null)
            {
                if (image.ImageFile != null)
                {
                    return File(image.ImageFile, "image/jpg");
                }
            }

            return null;
        }

        public ActionResult ShowPreviewVideoEdit(int id)
        {
            // Need to delay getting videos from the database because it throws
            // "The underlying provider failed on Open" if it happens too fast
            // Another fix could be MultipleActiveResultSets=True in connection string but it is not supported 
            var task = Task.Delay(1000).ContinueWith(t => Console.WriteLine(DateTime.Now));
            var video = _context.Videos.FirstOrDefault(i => i.CourseId == id && i.IsPreviewVideo == true);
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
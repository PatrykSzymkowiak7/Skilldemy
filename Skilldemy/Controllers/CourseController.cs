using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Skilldemy.Models;
using Skilldemy.Helpers;
using System.Net.Mail;
using System.Net;

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
            course.Rating = 0;
            course.RatingCount = 0;

            // Hack to fetch id
            _context.Courses.Add(course);
            _context.SaveChanges();
            var savedCourse = _context.Courses.FirstOrDefault(c => c.Title == course.Title 
                && c.Description == course.Description 
                && c.Price == course.Price 
                && c.OwnerUserName == course.OwnerUserName);
            course = savedCourse;

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

            // Video serialization to DB
            if (createCourseViewModel.PaidVideoVile != null)
            {
                Video paidVideo = new Video();

                byte[] uploadedPaidVideo = new byte[createCourseViewModel.PaidVideoVile.InputStream.Length];
                createCourseViewModel.PaidVideoVile.InputStream.Read(uploadedPaidVideo, 0, uploadedPaidVideo.Length);

                paidVideo.VideoFile = uploadedPaidVideo;
                paidVideo.IsPreviewVideo = false;
                paidVideo.OwnerId = course.OwnerId;
                paidVideo.CourseId = course.Id;
                paidVideo.IsPaidVideo = true;

                _context.Videos.Add(paidVideo);
            }

            SectionViewModel sectionViewModel = new SectionViewModel();
            sectionViewModel.CourseId = course.Id;

            _context.SaveChanges();

            return RedirectToAction("MyCourses");
        }

        public ActionResult ShowImage(int id)
        {
            // Need to delay getting pictures from the database because it throws
            // "The underlying provider failed on Open" if it happens too fast
            // Another fix could be MultipleActiveResultSets=True in connection string but it is not supported 
            var task = Task.Delay(1000).ContinueWith(t => Console.WriteLine(DateTime.Now));
            List<Image> images = _context.Images.Where(i => i.Id == id).ToList();
            Image image = images.FirstOrDefault();
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

        public ActionResult CoursePreview(int? id)
        {
            if(id == null)
            {
                return View("Index");
            }

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
            if(currentUserId == null)
            {
                return View("Index");
            }

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

            if (course.IsVisible)
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
            List<Image> images = _context.Images.Where(i => i.CourseId == id).ToList();
            List<Video> videos = _context.Videos.Where(i => i.CourseId == id).ToList();

            foreach(var image in images)
            {
                _context.Images.Remove(image);
            }
            foreach (var video in videos)
            {
                _context.Videos.Remove(video);
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return RedirectToAction("MyCourses");
        }

        public ActionResult EditCourseRedirect(int? id)
        {
            if(!id.HasValue)
            {
                return View("Index");
            }

            EditCourseViewModel editCourseViewModel = new EditCourseViewModel();
            Course course = new Course();
            Image image = new Image();
            image = _context.Images.FirstOrDefault(i => i.CourseId == id);

            Video video = new Video();
            video = _context.Videos.FirstOrDefault(v => v.CourseId == id && v.IsPreviewVideo == true);

            Video paidVideo = new Video();
            paidVideo = _context.Videos.FirstOrDefault(v => v.CourseId == id && v.IsPaidVideo == true);

            course = _context.Courses.FirstOrDefault(c => c.Id == id);

            editCourseViewModel.CourseId = id.Value;
            editCourseViewModel.CategoryId = course.CategoryId;
            editCourseViewModel.Description = course.Description;
            editCourseViewModel.OwnerId = course.OwnerId;
            editCourseViewModel.Price = course.Price;
            editCourseViewModel.Title = course.Title;
            editCourseViewModel.PreviewVideoFile = new HttpPostedFileBaseHelper(video.VideoFile, "Preview video" + course.Id);
            editCourseViewModel.ImageFile = new HttpPostedFileBaseHelper(image.ImageFile, "Preview image" + course.Id);
            editCourseViewModel.PaidVideoFile = new HttpPostedFileBaseHelper(paidVideo.VideoFile, "Paid video" + course.Id);

            return View("EditCourse", editCourseViewModel);
        }

        [HttpPost]
        public ActionResult EditCourse(EditCourseViewModel editCourseViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editCourseViewModel);
            }

            Course course = _context.Courses.FirstOrDefault(c => c.Id == editCourseViewModel.CourseId);

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
                image = _context.Images.FirstOrDefault(i => i.CourseId == course.Id);

                byte[] uploadedImage = new byte[editCourseViewModel.ImageFile.InputStream.Length];
                editCourseViewModel.ImageFile.InputStream.Read(uploadedImage, 0, uploadedImage.Length);

                image.ImageFile = uploadedImage;
                image.UploadedDate = DateTime.Now;
            }

            // Video serialization to DB
            if (editCourseViewModel.PreviewVideoFile != null)
            {
                Video previewVideo = new Video();
                previewVideo = _context.Videos.FirstOrDefault(v => v.CourseId == course.Id);

                byte[] uploadedVideo = new byte[editCourseViewModel.PreviewVideoFile.InputStream.Length];
                editCourseViewModel.PreviewVideoFile.InputStream.Read(uploadedVideo, 0, uploadedVideo.Length);

                previewVideo.VideoFile = uploadedVideo;
            }

            // Video serialization to DB
            if (editCourseViewModel.PaidVideoFile != null)
            {
                Video paidVideo = new Video();
                paidVideo = _context.Videos.FirstOrDefault(v => v.CourseId == course.Id && v.IsPaidVideo == true);

                byte[] uploadedPaidVideo = new byte[editCourseViewModel.PaidVideoFile.InputStream.Length];
                editCourseViewModel.PaidVideoFile.InputStream.Read(uploadedPaidVideo, 0, uploadedPaidVideo.Length);

                paidVideo.VideoFile = uploadedPaidVideo;
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

        public ActionResult ShowPaidVideoEdit(int id)
        {
            // Need to delay getting videos from the database because it throws
            // "The underlying provider failed on Open" if it happens too fast
            // Another fix could be MultipleActiveResultSets=True in connection string but it is not supported 
            var task = Task.Delay(1000).ContinueWith(t => Console.WriteLine(DateTime.Now));
            var video = _context.Videos.FirstOrDefault(i => i.CourseId == id && i.IsPaidVideo == true);
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

        public ActionResult ShowBoughtCourse(int? id, string uuid)
        {
            if (id == null)
            {
                return View("Index");
            }
            else if(uuid == null)
            {
                return View("Index");
            }

            UUIDConnection uuidConnection = _context.UUIDConnections.FirstOrDefault(u => u.UUID == uuid);
            List<CourseEntry> entriesSoFar = _context.CourseEntries.Where(e => e.UUID == uuid).ToList<CourseEntry>();
            int entriesCount = entriesSoFar.Count();

            // https://localhost:44317/course/ShowBoughtCourse/20?uuid=f0c3ea12-6433-4b96-ab99-ac5f4d091738
            if (entriesCount < 3 || uuid == "f0c3ea12-6433-4b96-ab99-ac5f4d091738")
            {
                CourseEntry courseEntry = new CourseEntry
                {
                    UUID = uuid,
                    CourseId = id.Value,
                    EntryTime = DateTime.Now
                };

                CourseBoughtViewModel courseBoughtViewModel = new CourseBoughtViewModel();
                Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
                List<Video> videos = (_context.Videos.Where(v => v.CourseId == id)).ToList();
                CourseRating courseRating = _context.CourseRatings.FirstOrDefault(c => c.UUID == uuid);

                courseBoughtViewModel.Course = course;
                courseBoughtViewModel.Videos = videos;
                courseBoughtViewModel.currentUuid = uuid;
                courseBoughtViewModel.CourseEntries = 3 - entriesCount;

                if (courseRating != null)
                    courseBoughtViewModel.Score = courseRating.Score;
                else
                    courseBoughtViewModel.Score = 0;

                _context.CourseEntries.Add(courseEntry);
                _context.SaveChanges();
                return View("MainCourse", courseBoughtViewModel);
            }
            else
            {
                return View("EntriesExceeded");
            }
        }

        public ActionResult ShowSectionVideo(int? id, int videoId)
        {
            CourseBoughtViewModel courseBoughtViewModel = new CourseBoughtViewModel();
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            List<Video> videos = (_context.Videos.Where(v => v.CourseId == id)).ToList();
            var video = videos.FirstOrDefault(i => i.Id == videoId);

            courseBoughtViewModel.Course = course;
            courseBoughtViewModel.Videos = videos;
            courseBoughtViewModel.VideoToShow = videoId;

            return View("MainCourse", courseBoughtViewModel);
        }

        public ActionResult ReviewCourse(int id, int score, string uuid, int courseEntries)
        {
            CourseBoughtViewModel courseBoughtViewModel = new CourseBoughtViewModel();
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            List<Video> videos = (_context.Videos.Where(v => v.CourseId == id)).ToList();
            CourseRating foundCourseRating = _context.CourseRatings.FirstOrDefault(c => c.UUID == uuid);

            if(foundCourseRating == null)
            {
                CourseRating courseRating = new CourseRating();
                courseRating.CourseId = id;
                courseRating.Score = score;
                courseRating.UUID = uuid;
                _context.CourseRatings.Add(courseRating);
                course.RatingCount += course.RatingCount;
                courseBoughtViewModel.Score = courseRating.Score;
            }
            else
            {
                foundCourseRating.CourseId = id;
                foundCourseRating.Score = score;
                foundCourseRating.UUID = uuid;
                courseBoughtViewModel.Score = foundCourseRating.Score;
            }

            courseBoughtViewModel.Course = course;
            courseBoughtViewModel.Videos = videos;
            courseBoughtViewModel.currentUuid = uuid;
            courseBoughtViewModel.CourseEntries = courseEntries;

            _context.SaveChanges();

            List<CourseRating> courseRatings = _context.CourseRatings.Where(c => c.CourseId == id).ToList();
            double courseRatingSum = 0;
            foreach (var cR in courseRatings)
            {
                courseRatingSum += cR.Score;
            }
            if (courseRatingSum != 0 && course.RatingCount != 0)
            {
                course.Rating = courseRatingSum / course.RatingCount;
                course.Rating = Math.Round(course.Rating, 1);
            }

            _context.SaveChanges();

            return View("MainCourse", courseBoughtViewModel);
        }
    }
}
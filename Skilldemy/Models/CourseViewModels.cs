using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skilldemy.Models
{
    public class CreateCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string OwnerId { get; set; }

        [Required]
        [Display(Name = "Cena")]
        [Range(0, 2000, ErrorMessage = "Wartość w tym polu musi być liczbą dodatnią, oraz nie może przekraczać 2000 zł")]
        public decimal Price { get; set; }

        public string HtmlAndJs { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Miniaturka")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name = "Wideo prezentujące kurs")]
        public HttpPostedFileBase PreviewVideoFile { get; set; }

        [CategoryDDLValidation]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
    }

    public class SectionViewModel
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Plik video")]
        public HttpPostedFileBase VideoFile { get; set; }

        [Required]
        [Display(Name = "Tytuł sekcji")]
        public string SectionTitle { get; set; }

        [Required]
        [Display(Name = "Opis sekcji")]
        public string SectionDescription { get; set; }

        public DateTime UploadedDate { get; set; }

        public bool IsPreviewVideo { get; set; }
    }

    public class CoursePreviewViewModel
    {
        public Course Course;
        public List<Video> Videos;
    }

    public class CourseBoughtViewModel
    {
        public Course Course;
        public List<Video> Videos;
    }

    public class EditCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string OwnerId { get; set; }

        [Required]
        [Display(Name = "Cena")]
        [Range(0, 2000, ErrorMessage = "Wartość w tym polu musi być liczbą dodatnią, oraz nie może przekraczać 2000 zł")]
        public decimal Price { get; set; }

        public string HtmlAndJs { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Miniaturka")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Wideo prezentujące kurs")]
        public HttpPostedFileBase PreviewVideoFile { get; set; }

        [CategoryDDLValidation]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
    }
}
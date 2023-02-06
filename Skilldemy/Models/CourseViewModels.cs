using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skilldemy.Models
{
    public class CreateCourseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Tytuł")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Opis")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string OwnerId { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Cena")]
        [Display(Name = "Cena")]
        [Range(1, 2000, ErrorMessage = "Wartość musi być z zakresu 1 - 2000 zł")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        public string HtmlAndJs { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Miniaturka")]
        [Display(Name = "Miniaturka")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Wideo prezentujące kurs")]
        [Display(Name = "Wideo prezentujące kurs")]
        public HttpPostedFileBase PreviewVideoFile { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Wideo Płatne")]
        [Display(Name = "Wideo Płatne")]
        public HttpPostedFileBase PaidVideoVile { get; set; }

        [CategoryDDLValidation]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
    }

    public class SectionViewModel
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Plik video")]
        [Display(Name = "Plik video")]
        public HttpPostedFileBase VideoFile { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Tytuł sekcji")]
        [Display(Name = "Tytuł sekcji")]
        public string SectionTitle { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Opis sekcji")]
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
        public int VideoToShow;
        public int CourseEntries;
        public int Score;
        public string currentUuid;
    }

    public class EditCourseViewModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Tytuł")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pole wymagane - opis")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string OwnerId { get; set; }

        [Required(ErrorMessage = "Pole wymagane - Cena")]
        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, 2000, ErrorMessage = "Wartość w tym polu musi być liczbą dodatnią, oraz nie może przekraczać 2000 zł")]
        public decimal? Price { get; set; }

        public string HtmlAndJs { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Miniaturka")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Wideo prezentujące kurs")]
        public HttpPostedFileBase PreviewVideoFile { get; set; }

        [Display(Name = "Wideo płatne")]
        public HttpPostedFileBase PaidVideoFile { get; set; }

        [CategoryDDLValidation]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
    }
}
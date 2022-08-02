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
        [Range(0, double.MaxValue, ErrorMessage = "Wartość w tym polu musi być liczbą dodatnią")]
        public double Price { get; set; }

        public string HtmlAndJs { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Miniaturka")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Wideo prezentujące kurs")]
        public HttpPostedFileBase PreviewVideoFile { get; set; }
    }

    public class EditCourseViewModel
    {

    }

    public class ManageSectionsViewModel
    {

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Skilldemy.Models
{
    public class Video
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        public int CourseId { get; set; }

        [Display(Name = "Plik video")]
        public byte[] VideoFile { get; set; }

        [Display(Name = "Tytuł sekcji")]
        public string SectionTitle { get; set; }

        [Display(Name = "Opis sekcji")]
        public string SectionDescription { get; set; }

        public DateTime UploadedDate { get; set; }

        public bool IsPreviewVideo { get; set; }

        public bool IsPaidVideo { get; set; }
    }
}
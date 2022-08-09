using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace Skilldemy.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public string OwnerUserName { get; set; }

        [Required]
        [Display(Name = "Cena")]
        [Range(0, double.MaxValue, ErrorMessage = "Wartość w tym polu musi być liczbą dodatnią")]
        public double Price { get; set; }

        public double Discount { get; set; }

        public double Rating { get; set; }

        public int Reviews { get; set; }

        public string Difficulty { get; set; }

        public string HtmlAndJs { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }
    }
}
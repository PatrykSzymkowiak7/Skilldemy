using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Skilldemy.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter course's title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter course's description")]
        public string Description { get; set; }

        public int OwnerId { get; set; }

        public List<Tag> Tags { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double Rating { get; set; }

        public int Reviews { get; set; }

        public string Difficulty { get; set; }
    }
}
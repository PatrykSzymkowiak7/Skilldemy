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

        public string Title { get; set; }

        public string Description { get; set; }

        public int OwnerId { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double Rating { get; set; }

        public int Reviews { get; set; }

        public string Difficulty { get; set; }
        public int temp { get; set; }
    }
}
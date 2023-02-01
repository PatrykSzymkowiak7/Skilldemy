using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skilldemy.Models
{
    public class CourseRating
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int Score { get; set; }

        public string UUID { get; set; }
    }
}
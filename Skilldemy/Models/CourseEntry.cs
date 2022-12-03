using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skilldemy.Models
{
    public class CourseEntry
    {
        public int Id { get; set; }

        public string UUID { get; set; }

        public int CourseId { get; set; }

        public DateTime EntryTime { get; set; }
    }
}
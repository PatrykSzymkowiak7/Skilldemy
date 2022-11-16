using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skilldemy.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string EmailAddress { get; set; }

        public DateTime Date { get; set; }

        public string PaymentStatus { get; set; }

        public string UUID { get; set; }
    }
}
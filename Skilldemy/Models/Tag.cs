using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Skilldemy.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter tag name")]
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
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

        public byte[] VideoFile { get; set; }

        public DateTime UploadedDate { get; set; }

        public bool IsPreviewVideo { get; set; }
    }
}
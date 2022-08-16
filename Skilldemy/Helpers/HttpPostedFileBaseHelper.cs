using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Skilldemy.Helpers
{
    public class HttpPostedFileBaseHelper : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public HttpPostedFileBaseHelper(byte[] fileBytes, string fileName)
        {
            this.fileBytes = fileBytes;
            this.InputStream = new MemoryStream(fileBytes);
            this.FileName = fileName;
        }

        public override int ContentLength => fileBytes.Length;
        public override string FileName { get; }
        public override string ContentType { get; } = "application/octet-stream";
        public override Stream InputStream { get; }
    }
}
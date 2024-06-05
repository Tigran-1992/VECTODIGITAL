using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VECTODIGITAL.Models
{
    public class FileUploadViewModel
    {
        public HttpPostedFileBase File { get; set; }
        public string FilePath { get; set; }
    }
}
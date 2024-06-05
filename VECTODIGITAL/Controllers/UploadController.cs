using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VECTODIGITAL.Models;

namespace VECTODIGITAL.Controllers
{
    public class UploadController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View(new FileUploadViewModel());
        }
        [HttpPost]
        public ActionResult UploadFile(FileUploadViewModel model)
        {
            try
            {
                if (model.File != null && model.File.ContentLength > 0)
                {
                    
                    string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; 
                    var extension = Path.GetExtension(model.File.FileName).ToLower();
                    if (!permittedExtensions.Contains(extension))
                    {
                        ViewBag.Message = "Invalid file type!";
                        return View(model);
                    }

                    if (model.File.ContentLength > 10 * 1024 * 1024) 
                    {
                        ViewBag.Message = "File size exceeded!";
                        return View(model);
                    }

                    string fileName = Path.GetFileNameWithoutExtension(model.File.FileName);
                    string uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), uniqueFileName);

                    model.File.SaveAs(path);
                    model.FilePath = Url.Content(Path.Combine("~/UploadedFiles", uniqueFileName));
                    ViewBag.Message = "File Uploaded Successfully!!";
                }
                else
                {
                    ViewBag.Message = "No file selected!";
                }
            }
            catch (Exception ex)
            {
                
                ViewBag.Message = $"File upload failed! Error: {ex.Message}";
            }

            return View(model);
        }
    }
}
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMVCProject.Controllers
{
    public class GalleryController : Controller
    {
        ImageFileManager imageFileManager = new ImageFileManager(new EfImageFileDal());

        public ActionResult GalleryList()
        {
            var values= imageFileManager.GetList();
            return View(values);
        }
    }
}
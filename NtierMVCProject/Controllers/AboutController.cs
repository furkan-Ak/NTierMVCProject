using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMVCProject.Controllers
{
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());
        public ActionResult AboutList()
        {
            var values = aboutManager.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            aboutManager.AboutAddBl(about);
            return RedirectToAction("AboutList");

        }
        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
    }
}
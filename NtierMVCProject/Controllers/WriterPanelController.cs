﻿using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMVCProject.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
       
        public ActionResult WriterProfile()
        {
            return View();
        }

        public ActionResult MyHeading(string p)
        {
            Context c = new Context();  
            p = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var values = headingManager.GetListByWriter(writeridinfo);
            return View(values);
        }
        [HttpGet]

        public ActionResult NewHeading()
        {
           
            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName, // text member adını tutar
                                                      Value = x.CategoryID.ToString(), // value member idsini tutar
                                                  }).ToList();
            ViewBag.VBcategory = valuecategory;

            return View();
        }
        [HttpPost]
        public ActionResult NewHeading( Heading heading,string a)
        {
            Context c = new Context();
            a = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == a).Select(y => y.WriterID).FirstOrDefault();
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterID = writeridinfo;
            heading.HeadingStatus = true;
            headingManager.HeadingAddBl(heading);
            return RedirectToAction("MyHeading");
        }
        [HttpGet]
        public ActionResult EditMyHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName, // text member adını tutar
                                                      Value = x.CategoryID.ToString(), // value member idsini tutar

                                                  }).ToList();
            ViewBag.VBcategory = valuecategory;
            var value = headingManager.GetByID(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult EditMyHeading(Heading heading)
        {

            headingManager.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }
        public ActionResult DeleteMyHeading(int id)
        {
            var value = headingManager.GetByID(id);
            value.HeadingStatus = false;
            headingManager.HeadingDelete(value);
            return RedirectToAction("MyHeading");
        }
    }
}
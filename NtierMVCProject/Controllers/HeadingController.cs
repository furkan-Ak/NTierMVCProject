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
    public class HeadingController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager=new WriterManager(new EfWriterDal());
        public ActionResult GetHeadingList()
        {
            var values= headingManager.GetList();
            return View(values);
        }


        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> valuecategory=(from x in categoryManager.GetList()
                                                select new SelectListItem
                                                {
                                                    Text=x.CategoryName, // text member adını tutar
                                                    Value=x.CategoryID.ToString(), // value member idsini tutar

                                                }).ToList();
            List<SelectListItem> valuewriter=(from y in writerManager.GetList()
                                              select new SelectListItem
                                              {
                                                  Text=y.WriterName + "  " + y.WriterSurname,
                                                  Value=y.WriterID.ToString(),
                                              }).ToList();

            ViewBag.VBcategory = valuecategory;
            ViewBag.VBwriter = valuewriter;
            return View();
        }
        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            headingManager.HeadingAddBl(heading);
            return RedirectToAction("GetHeadingList");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName, // text member adını tutar
                                                      Value = x.CategoryID.ToString(), // value member idsini tutar

                                                  }).ToList();
            ViewBag.VBcategory = valuecategory;
            var value=headingManager.GetByID(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {

            headingManager.HeadingUpdate(heading);
            return RedirectToAction("GetHeadingList");
        }
        public ActionResult DeleteHeading(int id)
        {
            var value = headingManager.GetByID(id);
            value.HeadingStatus = false;
            headingManager.HeadingDelete(value);
            return RedirectToAction("GetHeadingList");
        }
    }
}
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMVCProject.Controllers
{
    public class WriterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        WriterValidator validator = new WriterValidator();

        public ActionResult GetWriterList()
        {
            var writervalues = writerManager.GetList();
            return View(writervalues);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(Writer writer)
        {
            ValidationResult validationResult = validator.Validate(writer);
            if (validationResult.IsValid)
            {
                writerManager.WriterAdd(writer);
                return RedirectToAction("GetWriterList");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writer= writerManager.GetByID(id);
            return View(writer);
        }
        [HttpPost]
        public ActionResult EditWriter(Writer writer)
        {
            ValidationResult validationResult = validator.Validate(writer);
            if (validationResult.IsValid)
            {
                writerManager.WriterUpdate(writer);

                return RedirectToAction("GetWriterList");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
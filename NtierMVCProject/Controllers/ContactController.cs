using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMVCProject.Controllers
{
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager(new EfContactDal());
        ContactValidator validator = new ContactValidator();
        public ActionResult ContactList()
        {
            var values = contactManager.GetList();
            return View(values);
        }
        public ActionResult GetContactDetail(int id)
        {
            var values= contactManager.GetByID(id);
            return View(values);
        }
        public PartialViewResult ContactMenu()
        {
            return PartialView();
        }
    }
}
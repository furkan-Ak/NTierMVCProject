using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NtierMVCProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult LoginMain()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginMain(Admin admin)
        {
            Context c= new Context();
            var admininfo = c.Admins.FirstOrDefault(x => x.AdminName == admin.AdminName && x.AdminPassword==admin.AdminPassword);
            if (admininfo != null)
            {
                FormsAuthentication.SetAuthCookie(admininfo.AdminName, false);
                Session["AdminName"] = admininfo.AdminName;
                return RedirectToAction("CategoryList", "AdminCategory");
            }
            else
            {
                return RedirectToAction("LoginMain");
            }
        }
        [HttpGet]
        public ActionResult WriterLoginMain()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLoginMain(Writer writer)
        {
            Context c = new Context();
            var writerinfo = c.Writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);
            if (writerinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writerinfo.WriterMail, false);
                Session["WriterMail"] = writerinfo.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                return RedirectToAction("WriterLoginMain");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Heading","Default");
        }
    }
}
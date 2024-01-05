using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMVCProject.Controllers
{
    public class ContentController : Controller
    {
        ContentManager contentManager = new ContentManager(new EfContentDal());
        public ActionResult ContentByHeading(int id)
        {   
            var values= contentManager.GetListByHeadingID(id);
            return View(values);
        }
        public ActionResult GettAllContent(string p="") // content filter
        {
            var values = contentManager.GetList(p);
            return View(values);
        }
    }
}
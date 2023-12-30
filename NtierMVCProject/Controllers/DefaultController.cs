using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMVCProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        ContentManager contentManager= new ContentManager(new EfContentDal());
        WriterManager writerManager= new WriterManager(new EfWriterDal());
        public ActionResult Heading()
        {
            var headinglist= headingManager.GetList();
            return View(headinglist);
        }
        public PartialViewResult Content(int id=0)
        {
            var contentlist= contentManager.GetListByHeadingID(id);
            return PartialView(contentlist);
        }
    }
}
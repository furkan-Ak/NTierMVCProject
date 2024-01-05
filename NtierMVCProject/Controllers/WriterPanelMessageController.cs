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
    public class WriterPanelMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult WriterMessageInbox()
        {
            string writermailinfo = (string)Session["WriterMail"];
            var messagelist = messageManager.GetListInbox(writermailinfo);
            return View(messagelist);
        }
        public ActionResult WriterMessageSendbox()
        {
            string writermailinfo = (string)Session["WriterMail"];
            var messagelist = messageManager.GetListSendbox(writermailinfo);
            return View(messagelist);
        }
        public ActionResult WriterGetInboxMessageDetail(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);
        }
        public ActionResult WriterGetSendBoxMessageDetail(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult WriterNewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterNewMessage(Message message)
        {
            string writermailinfo = (string)Session["WriterMail"];
            ValidationResult validationResult = messageValidator.Validate(message);
            if (validationResult.IsValid)
            {
                message.SenderMail = writermailinfo;
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                messageManager.MessageAddBl(message);
                return RedirectToAction("WriterMessageSendbox");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return RedirectToAction("MessageSendbox");
        }
        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
        
    }
}
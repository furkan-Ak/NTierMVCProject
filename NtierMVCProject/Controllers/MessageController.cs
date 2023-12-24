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
    public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();

        [Authorize]
        public ActionResult MessageInbox()
        {
            var messagelist = messageManager.GetListInbox();
            return View(messagelist);
        }
        public ActionResult MessageSendbox()
        {
            var messagelist = messageManager.GetListSendbox();
            return View(messagelist);
        }
        public ActionResult GetInboxMessageDetail(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);
        }
        public ActionResult GetSendBoxMessageDetail(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            ValidationResult validationResult = messageValidator.Validate(message);
            if (validationResult.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                messageManager.MessageAddBl(message);
                return RedirectToAction("MessageSendbox");
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
    }
}
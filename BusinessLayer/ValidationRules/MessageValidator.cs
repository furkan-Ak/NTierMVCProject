using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı Mailini Boş Geçemezsiniz!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konuyu Boş Geçemezsiniz!");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesajı Boş Geçemezsiniz!");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("Lütfen 100 Karakterden Fazla Veri Girişi Yapmayınız!");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen En Az 3 Karakter Giriniz!");
        }
    }
}

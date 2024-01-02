using AspNetCoreHero.ToastNotification.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OnlineEgitimClient.Areas.Admin.Models;

namespace OnlineEgitimClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MailController : Controller
    {
        private readonly INotyfService _notyfService;
        public MailController(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ReceiverMail = TempData["ReceiverMail"] as string;
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            try
            {
                MimeMessage mimeMessage = new MimeMessage();

                MailboxAddress mailboxAddressFrom = new MailboxAddress("OnlineEgitimAdmin", "kayatablet2018@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
                mimeMessage.To.Add(mailboxAddressTo);

                mimeMessage.Subject = mailRequest.Subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = mailRequest.Body;
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("kayatablet2018@gmail.com", "wjrujeagtnhlzgbr");
                client.Send(mimeMessage);
                client.Disconnect(true);

                _notyfService.Success("Mailiniz Başarıyla Gönderildi");
            }
            catch (Exception ex)
            {
                _notyfService.Error("Mailiniz Gönderilemedi Eroor = "+ ex);
            }

            return RedirectToAction("Index");
        }
    }
}

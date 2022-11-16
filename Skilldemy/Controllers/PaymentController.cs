using Skilldemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace Skilldemy.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        private ApplicationDbContext _context;

        public PaymentController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult PreparePayment(int courseId, string courseName, decimal price)
        {
            PaymentViewModel paymentPreparation = new PaymentViewModel();
            paymentPreparation.Crc = courseId;
            paymentPreparation.Description = courseName;
            paymentPreparation.Amount = price;
            return View("PreparePayment", paymentPreparation);
        }

        public ActionResult CreatePayment(PaymentViewModel paymentPreparation)
        {
            if (!ModelState.IsValid)
            {
                return View(paymentPreparation);
            }

            string amount = paymentPreparation.Amount.ToString().Replace(",", ".");
            string description = paymentPreparation.Description.Replace(" ", "%20");
            paymentPreparation.Name = String.Format("{0}%20{1}", paymentPreparation.FirstName, paymentPreparation.LastName);
            string implode = String.Format("{0}&{1}&{2}&{3}", paymentPreparation.Id.ToString(), amount, paymentPreparation.Crc.ToString(), paymentPreparation.Code);

            byte[] source;
            byte[] hash;
            source = ASCIIEncoding.ASCII.GetBytes(implode);
            hash = new MD5CryptoServiceProvider().ComputeHash(source);
            StringBuilder hashToHex = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                hashToHex.Append(hash[i].ToString("X2"));
            }
            string md5sum = hashToHex.ToString().ToLower();

            Guid guid = Guid.NewGuid();
            string guidStr = guid.ToString();

            Payment payment = new Payment();
            payment.CourseId = paymentPreparation.Crc;
            payment.Date = DateTime.Now;
            payment.EmailAddress = paymentPreparation.EmailAddress;
            payment.UUID = guidStr;

            paymentPreparation.Return_url = Url.Action("PaymentSuccess", "Payment", new { UUID = payment.UUID }, this.Request.Url.Scheme);
            paymentPreparation.Return_error_url = Url.Action("PaymentFailure", "Payment", new { UUID = payment.UUID }, this.Request.Url.Scheme);

            StringBuilder link = new StringBuilder();
            link.Append("https://secure.tpay.com");
            link.Append("?id=" + paymentPreparation.Id);
            link.Append("&amount=" + amount);
            link.Append("&description=" + description);
            link.Append("&name=" + paymentPreparation.Name);
            link.Append("&email=" + paymentPreparation.EmailAddress);
            link.Append("&return_url=" + paymentPreparation.Return_url);
            link.Append("&return_error_url=" + paymentPreparation.Return_error_url);
            link.Append("&crc=" + paymentPreparation.Crc.ToString());
            link.Append("&md5sum=" + md5sum);

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return Redirect(link.ToString());
        }

        public ActionResult PaymentSuccess(string UUID)
        {
            return View("PaymentSuccess");
        }

        public ActionResult PaymentFailure(string UUID)
        {
            return View();
        }
    }
}
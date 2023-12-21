using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Service;
using System.Text;
using System.Transactions;

namespace OnlineEgitimClient.Controllers
{
    public class PaymentController : Controller
    {
        private readonly BasketService _basketService;
        private readonly CustomHttpClient _customHttpClient;
        public PaymentController(BasketService basketService, CustomHttpClient customHttpClient)
        {
            _basketService = basketService;
            _customHttpClient = customHttpClient;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
           
            Options options = new Options();
            options.ApiKey = "sandbox-KQwrxCOR3MJMtODwGWItpeDtsu5N4Bty";
            options.SecretKey = "sandbox-AJxgH1pVSADUoBqst4ivdKPcLbTYPDT8";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = _basketService.TotalPrice().ToString().Replace(",", ".");
            request.PaidPrice = _basketService.TotalPrice().ToString().Replace(",", ".");
            request.Currency = Currency.TRY.ToString();
            request.BasketId = "B67832";
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://localhost:7107/Payment/SuccessPayment/";

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@example.com";
            buyer.IdentityNumber = "74300864791";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";

            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Email";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";

            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Email";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";

            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            var basketCourse = _basketService.GetBasketCourse();
            foreach (var item in basketCourse)
            {
                BasketItem Item = new BasketItem();
                Item.Id = item.Id.ToString();
                Item.Name = item.Title;
                Item.Category1 = "Kurs";
                Item.ItemType = BasketItemType.VIRTUAL.ToString();
                Item.Price = item.Price.ToString().Replace(",", ".");

                basketItems.Add(Item);
            }
            request.BasketItems = basketItems;

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
          
            // iyzico'dan dönen sonucu konsola yazdırma
            Console.WriteLine(checkoutFormInitialize.Status);
            Console.WriteLine(checkoutFormInitialize.ErrorCode);
            Console.WriteLine(checkoutFormInitialize.ErrorMessage);
            Console.WriteLine(checkoutFormInitialize.ErrorGroup);
            Console.WriteLine(checkoutFormInitialize.CheckoutFormContent);
          
            ViewBag.CheckoutFormContent = checkoutFormInitialize.CheckoutFormContent;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SuccessPayment()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            await _basketService.BuyCourse(userId);
            return View();
        }

    }
}

namespace OnlineEgitimClient.Dtos.PaymentDto
{
    public class PaymentDto
    { 
        public class Buyer
        {
            //buyer.Id = "BY789";
            //buyer.Name = "John";
            //buyer.Surname = "Doe";
            //buyer.GsmNumber = "+905350000000";
            //buyer.Email = "email@example.com";
            ////buyer.IdentityNumber = "74300864791";
            ////buyer.LastLoginDate = "2023-12-05 12:43:35";
            ////buyer.RegistrationDate = "2023-01-05 15:12:09";
            //buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            ////buyer.Ip = "85.34.78.112";
            //buyer.City = "Istanbul";
            //buyer.Country = "Turkey";
            ////buyer.ZipCode = "34732";
            public string  BuyerName { get; set; }
            public string  BuyerSurname { get; set; }
            public string  BuyerGsmNo { get; set; }
            public string  BuyerEmail { get; set; }
            public string  BuyerAdres { get; set; }
            public string  BuyerCity { get; set; }
        }
    }
}

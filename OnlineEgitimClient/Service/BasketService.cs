using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Dtos.PurchasedCourseDto;

namespace OnlineEgitimClient.Service
{
    public class BasketService
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<ListCourseDto> courseList = new List<ListCourseDto>();

        public BasketService(CustomHttpClient customHttpClient, IHttpContextAccessor httpContextAccessor)
        {
            _customHttpClient = customHttpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        private List<ListCourseDto> GetCourseListFromSession()
        {
            var courseListJson = _httpContextAccessor?.HttpContext?.Request.Cookies["UserCourseList"];
            if (courseListJson != null)
            {
                return JsonConvert.DeserializeObject<List<ListCourseDto>>(courseListJson);
            }
            return new List<ListCourseDto>();
        }
        private void SetCourseListToSession(List<ListCourseDto> courseList)
        {
            var courseListJson = JsonConvert.SerializeObject(courseList);
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("UserCourseList", courseListJson);
        }
        public async Task AddBasketCourse(int id)
        {
            var courseList = GetCourseListFromSession();
            var existingCourse = courseList.FirstOrDefault(c => c.Id == id);

            if (existingCourse == null)
            {
                var responseMessage = await _customHttpClient.Get(new() { Controller = "Course" }, id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ListCourseDto>(jsonData);
                    courseList.Add(values);
                    SetCourseListToSession(courseList);
                }
            }
            else
            {
                Console.WriteLine("Bu ID'ye sahip kurs zaten listenizde bulunmaktadır.");
            }
        }
       
        public List<ListCourseDto> GetBasketCourse()
        {
            return GetCourseListFromSession();
        }

        public void DeleteBasketCourse(int id)
        {
            var courseList = GetCourseListFromSession();
            var item = courseList.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                courseList.Remove(item);
                SetCourseListToSession(courseList);
            }
        }
        public void ClearBasketCourse()
        {
            var courseList = GetCourseListFromSession();
            courseList.Clear(); // Listenin içeriğini temizle
            SetCourseListToSession(courseList);
        }
        public double TotalPrice()
        {
            var courseList = GetCourseListFromSession();
            double totalPrice = 0;
            foreach (var item in courseList)
            {
                totalPrice += item.Price;
            }
            return totalPrice;
        }
        public int TotalCourse()
        {
            var courseList = GetCourseListFromSession();
            return courseList.Count();
        }

        public async Task BuyCourse(int userId)
        {
            var courseList = GetCourseListFromSession();
            var purchasedCoursesDto = new AddPurchasedCourseDto();
            purchasedCoursesDto.AppUserID = userId;

            foreach (var item in courseList)
            {
                purchasedCoursesDto.CourseID = item.Id;

                var responseMessage = await _customHttpClient.Post<AddPurchasedCourseDto>(new() { Controller = "PurchasedCourse" }, purchasedCoursesDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    //Console.WriteLine($"Kurs id {item.Id} başarılı bir şekilde {userId} ye ait kullanıcıya eklenmiştir");
                }

                else
                {
                    //Console.WriteLine($"kurs id {item.Id} {userId} id li kullanıcya eklenirken hata oluştu");
                }
            }
            ClearBasketCourse();
        }
    }
}
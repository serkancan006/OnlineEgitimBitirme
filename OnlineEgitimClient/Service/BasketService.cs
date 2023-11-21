using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;

namespace OnlineEgitimClient.Service
{
    public class BasketService
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly List<ListCourseDto> courseList = new List<ListCourseDto>();

        public BasketService(CustomHttpClient customHttpClient)
        {
            _customHttpClient = customHttpClient;
        }

        public async Task AddBasketCourse(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ListCourseDto>(jsonData);
                courseList.Add(values);
            }
        }

        public List<ListCourseDto> GetBasketCourse()
        {
            return courseList;
        }

        public void DeleteBasketCourse(int id)
        {
            var item = courseList.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                courseList.Remove(item);
            }
        }
        public void ClearBasketCourse()
        {
            courseList.Clear();
        }
        public decimal TotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var item in courseList)
            {
                totalPrice += item.Price;
            }
            return totalPrice;
        }
        public int TotalCourse()
        {
            return courseList.Count();
        }
    }
}

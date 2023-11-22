using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineEgitimClient.Dtos.CourseDto;

namespace OnlineEgitimClient.Service
{
    public class BasketService
    {
        private readonly CustomHttpClient _customHttpClient;
        private readonly SessionService _sessionService;
        private List<ListCourseDto> courseList;

        public BasketService(CustomHttpClient customHttpClient, SessionService sessionService)
        {
            _customHttpClient = customHttpClient;
            _sessionService = sessionService;
            courseList = _sessionService.GetValue<List<ListCourseDto>>("Basket") ?? new List<ListCourseDto>();
        }

        public async Task AddBasketCourse(int id)
        {
            var responseMessage = await _customHttpClient.Get(new() { Controller = "Course" }, id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ListCourseDto>(jsonData);
                courseList.Add(values);
                _sessionService.SetValue("Basket", courseList);
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
                _sessionService.SetValue("Basket", courseList);
            }
        }
        public void ClearBasketCourse()
        {
            courseList.Clear();
            _sessionService.SetValue("Basket", courseList);
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
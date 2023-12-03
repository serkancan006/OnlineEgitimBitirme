namespace OnlineEgitimClient.Dtos.CourseDto
{
    public class WidgetCourseByUserDto
    {
        public int appUserID { get; set; }
        public int courseID { get; set; }
        public string imageUrl { get; set; }
        public string title { get; set; }
        public string language { get; set; }
        public string level { get; set; }
        public double price { get; set; }
        public int courseViewCountLog { get; set; }
    }
}

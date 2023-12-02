namespace OnlineEgitimClient.Dtos.CourseDto
{
    public class TopPurchasedCourseDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public int PurchaseCount { get; set; }
    }
}

namespace OnlineEgitimClient.Dtos.WidgetClickLogDto
{
    public class UpdateWidgetClickLogDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public int AppUserID { get; set; }
        public int CourseID { get; set; }
    }
}

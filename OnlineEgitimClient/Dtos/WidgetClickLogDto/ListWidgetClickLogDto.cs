namespace OnlineEgitimClient.Dtos.WidgetClickLogDto
{
    public class ListWidgetClickLogDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }

        public int AppUserID { get; set; }
        public int CourseID { get; set; }
    }
}

namespace OnlineEgitimClient.Dtos.CourseVideoDto
{
    public class ListCourseVideoDto
    {
        public int id { get; set; }
        public DateTime createdDate { get; set; }
        //virtual public DateTime UpdatedDate { get; set; }
        public bool status { get; set; }

        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileDisplayName { get; set; }
        public int courseID { get; set; }
        //public Course Course { get; set; }
    }
}

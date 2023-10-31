namespace OnlineEgitimClient.Dtos.LocationDto
{
    public class ListLocationDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }

        public string Address { get; set; }
    }
}

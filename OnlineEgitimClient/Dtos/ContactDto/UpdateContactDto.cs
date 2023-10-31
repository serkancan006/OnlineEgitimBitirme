namespace OnlineEgitimClient.Dtos.ContactDto
{
    public class UpdateContactDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public string Mail { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string MapLocation { get; set; }
    }
}

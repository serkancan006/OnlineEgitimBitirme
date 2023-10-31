using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.ContactDto
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

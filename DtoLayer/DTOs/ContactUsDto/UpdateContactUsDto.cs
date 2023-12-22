﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DTOs.ContactUsDto
{
    public class UpdateContactUsDto
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        public string Name { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

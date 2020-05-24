using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dtos
{
    public class UserAddDto
    {
        public string Account { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AddressDetail { get; set; }
    }
}

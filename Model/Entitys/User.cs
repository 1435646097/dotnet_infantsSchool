using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class User
    {
        public User()
        {
            Grade = new HashSet<Grade>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public DateTime? Brthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AddressDetail { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Grade> Grade { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}

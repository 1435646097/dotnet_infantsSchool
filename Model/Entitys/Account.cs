using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class Account
    {
        public Account()
        {
            AccountRole = new HashSet<AccountRole>();
            Grade = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string Account1 { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public DateTime? Brthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AddressDetail { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<AccountRole> AccountRole { get; set; }
        public virtual ICollection<Grade> Grade { get; set; }
    }
}

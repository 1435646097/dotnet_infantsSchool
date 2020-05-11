using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class AccountRole
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? Role { get; set; }

        public virtual Account Account { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}

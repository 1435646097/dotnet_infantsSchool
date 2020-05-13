using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? Role { get; set; }

        public virtual User Account { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}

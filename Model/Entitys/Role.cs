using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class Role
    {
        public Role()
        {
            AccountRole = new HashSet<AccountRole>();
            RoleAction = new HashSet<RoleAction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<AccountRole> AccountRole { get; set; }
        public virtual ICollection<RoleAction> RoleAction { get; set; }
    }
}

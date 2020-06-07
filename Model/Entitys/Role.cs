using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class Role
    {
        public Role()
        {
            RoleAction = new HashSet<RoleAction>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<RoleAction> RoleAction { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}

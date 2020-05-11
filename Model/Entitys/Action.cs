using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class Action
    {
        public Action()
        {
            RoleAction = new HashSet<RoleAction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int? Pid { get; set; }
        public string Remark { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<RoleAction> RoleAction { get; set; }
    }
}

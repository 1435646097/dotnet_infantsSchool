using System;
using System.Collections.Generic;

namespace dotnet_infantsSchool.Entitys
{
    public partial class ActionType
    {
        public ActionType()
        {
            Action = new HashSet<Action>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Action> Action { get; set; }
    }
}

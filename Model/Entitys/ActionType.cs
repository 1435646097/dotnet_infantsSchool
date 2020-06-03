using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class ActionType
    {
        public ActionType()
        {
            Action = new HashSet<Action>();
        }

        public int Id { get; set; }
        public string ActionType1 { get; set; }

        public virtual ICollection<Action> Action { get; set; }
    }
}

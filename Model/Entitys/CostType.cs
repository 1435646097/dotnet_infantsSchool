using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class CostType
    {
        public CostType()
        {
            GradeCost = new HashSet<GradeCost>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<GradeCost> GradeCost { get; set; }
    }
}

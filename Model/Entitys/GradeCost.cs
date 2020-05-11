using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class GradeCost
    {
        public int Id { get; set; }
        public bool? IsEpense { get; set; }
        public int? CostTypeId { get; set; }
        public string Remark { get; set; }
        public int? GradeId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDelete { get; set; }

        public virtual CostType CostType { get; set; }
        public virtual Grade Grade { get; set; }
    }
}

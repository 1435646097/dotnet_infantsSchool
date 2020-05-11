using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public int? GradeId { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Grade Grade { get; set; }
    }
}

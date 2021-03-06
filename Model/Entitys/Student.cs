﻿using System;

namespace Model.Entitys
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public int? GradeId { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Grade Grade { get; set; }
    }
}
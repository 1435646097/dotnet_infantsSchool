using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class Grade
    {
        public Grade()
        {
            Activity = new HashSet<Activity>();
            GradeCost = new HashSet<GradeCost>();
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? UserId { get; set; }
        public bool? IsDelete { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<GradeCost> GradeCost { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
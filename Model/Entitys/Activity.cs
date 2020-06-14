using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityPicture = new HashSet<ActivityPicture>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public string Remark { get; set; }
        public int? GradeId { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual ICollection<ActivityPicture> ActivityPicture { get; set; }
    }
}

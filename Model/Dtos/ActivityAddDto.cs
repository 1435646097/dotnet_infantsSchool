using Model.Entitys;
using System;
using System.Collections.Generic;

namespace Model.Dtos
{
    public class ActivityAddDto
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int GradeId { get; set; }
        public string Remark { get; set; }
        public List<ActivityPicture> Pics { get; set; }
    }
}
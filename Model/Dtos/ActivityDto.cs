using System;
using System.Collections.Generic;

namespace Model.Dtos
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public string Remark { get; set; }
        public string OnePicture { get; set; }
        public List<string> PictureList { get; set; }
        public int GradeId { get; set; }
    }
}
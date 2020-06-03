using System;

namespace Model.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string TeacherName { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
    }
}

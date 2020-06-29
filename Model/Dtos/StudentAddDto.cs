using System;

namespace Model.Dtos
{
    public class StudentAddDto
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int GradeId { get; set; }
        public bool? IsDelete { get; set; }
    }
}
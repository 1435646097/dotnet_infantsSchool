using System;

namespace Model.Dtos
{
    public class StudentEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string GradeId { get; set; }
    }
}
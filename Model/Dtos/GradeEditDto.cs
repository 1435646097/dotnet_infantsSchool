﻿using System;

namespace Model.Dtos
{
    public class GradeEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dtos
{
    public class ActionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int Pid { get; set; }
        public string Remark { get; set; }
        public int Level { get; set; }
        public bool IsDelete { get; set; }
    }
}

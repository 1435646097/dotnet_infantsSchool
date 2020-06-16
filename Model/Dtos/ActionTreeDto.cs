using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dtos
{
    public class ActionTreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pid { get; set; }
        public List<ActionTreeDto> Children { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
    }
}

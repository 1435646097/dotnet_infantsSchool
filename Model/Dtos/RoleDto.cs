using System.Collections.Generic;

namespace Model.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public List<ActionTreeDto> Children { get; set; }
        public bool IsDelete { get; set; }
    }
}
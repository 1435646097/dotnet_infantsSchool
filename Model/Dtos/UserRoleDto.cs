using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dtos
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }
    }
}

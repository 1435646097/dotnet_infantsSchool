using IRepository;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RoleActionRepository:BaseRepository<RoleAction>,IRoleActionRepository
    {
        public RoleActionRepository(InfantsSchoolSystemContext infantsSchoolSystemContext):base(infantsSchoolSystemContext)
        {

        }
    }
}

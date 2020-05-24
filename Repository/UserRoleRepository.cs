﻿using IRepository;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UserRoleRepository:BaseRepository<UserRole>,IUserRoleRepository
    {
        public UserRoleRepository(InfantsSchoolSystemContext infantsSchoolSystemContext):base(infantsSchoolSystemContext)
        {

        }
    }
}

using IRepository;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ActionRepository:BaseRepository<Model.Entitys.Action>,IActionRepository
    {
        public ActionRepository(InfantsSchoolSystemContext infantsSchoolSystemContext):base(infantsSchoolSystemContext)
        {

        }
    }
}

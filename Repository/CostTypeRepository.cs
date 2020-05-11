using IRepository;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CostTypeRepository : BaseRepository<CostType>, ICostTypeRepository
    {
        public CostTypeRepository(InfantsSchoolSystemContext infantsSchoolSystemContext)
        {
            base.InfantsSchoolSystemContext = infantsSchoolSystemContext;
        }
    }
}
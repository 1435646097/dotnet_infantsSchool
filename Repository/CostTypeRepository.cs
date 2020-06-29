using IRepository;
using Model.Entitys;

namespace Repository
{
    public class CostTypeRepository : BaseRepository<CostType>, ICostTypeRepository
    {
        public CostTypeRepository(InfantsSchoolSystemContext infantsSchoolSystemContext) : base(infantsSchoolSystemContext)
        {
        }
    }
}
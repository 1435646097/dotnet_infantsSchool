using IRepository;
using Model.Entitys;

namespace Repository
{
    public class RoleActionRepository : BaseRepository<RoleAction>, IRoleActionRepository
    {
        public RoleActionRepository(InfantsSchoolSystemContext infantsSchoolSystemContext) : base(infantsSchoolSystemContext)
        {
        }
    }
}
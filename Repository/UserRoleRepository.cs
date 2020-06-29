using IRepository;
using Model.Entitys;

namespace Repository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(InfantsSchoolSystemContext infantsSchoolSystemContext) : base(infantsSchoolSystemContext)
        {
        }
    }
}
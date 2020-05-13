using IRepository;
using Model.Entitys;

namespace Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(InfantsSchoolSystemContext infantsSchoolSystemContext) : base(infantsSchoolSystemContext)
        {
        }
    }
}
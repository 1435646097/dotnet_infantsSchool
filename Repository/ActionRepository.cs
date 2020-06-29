using IRepository;
using Model.Entitys;

namespace Repository
{
    public class ActionRepository : BaseRepository<Model.Entitys.Action>, IActionRepository
    {
        public ActionRepository(InfantsSchoolSystemContext infantsSchoolSystemContext) : base(infantsSchoolSystemContext)
        {
        }
    }
}
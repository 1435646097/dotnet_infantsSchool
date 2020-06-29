using IRepository;
using Model.Entitys;

namespace Repository
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(InfantsSchoolSystemContext infantsSchoolSystemContext) : base(infantsSchoolSystemContext)
        {
        }
    }
}
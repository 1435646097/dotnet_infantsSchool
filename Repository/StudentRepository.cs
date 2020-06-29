using IRepository;
using Model.Entitys;

namespace Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(InfantsSchoolSystemContext infantsSchoolSystemContext) : base(infantsSchoolSystemContext)
        {
        }
    }
}
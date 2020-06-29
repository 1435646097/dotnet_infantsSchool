using IRepository;
using IServices;
using Model.Entitys;
using Model.Helper;
using Model.Params;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class StudentServices : BaseServices<Student>, IStudentServices
    {
        private readonly IStudentRepository _studentRepository;

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            base.CurrentRepository = _studentRepository;
        }

        public async Task<PagedList<Student>> GetStudentPaged(StudentParams studentParams)
        {
            IQueryable<Student> temp = _studentRepository.GetEntitys();
            if (studentParams.GradeId > 0)
            {
                temp = temp.Where(t => t.GradeId == studentParams.GradeId);
            }
            if (!string.IsNullOrWhiteSpace(studentParams.Name))
            {
                temp = temp.Where(t => t.Name.Contains(studentParams.Name));
            }
            return await PagedList<Student>.CreatePagedList(temp, studentParams.PageSize, studentParams.PageNum);
        }
    }
}
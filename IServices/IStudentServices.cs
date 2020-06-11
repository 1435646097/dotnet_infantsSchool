using Model.Entitys;
using Model.Helper;
using Model.Params;
using System.Threading.Tasks;

namespace IServices
{
    public interface IStudentServices : IBaseIServices<Student>
    {
        Task<PagedList<Student>> GetStudentPaged(StudentParams studentParams);
    }
}

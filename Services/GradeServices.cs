using IRepository;
using IServices;
using Model.Entitys;

namespace Services
{
    public class GradeServices : BaseServices<Grade>, IGradeServices
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeServices(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
            base.CurrentRepository = _gradeRepository;
        }
    }
}
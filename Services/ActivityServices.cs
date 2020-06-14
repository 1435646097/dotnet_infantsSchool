using IRepository;
using IServices;
using Model.Dtos;
using Model.Entitys;
using System.Threading.Tasks;

namespace Services
{
    public class ActivityServices : BaseServices<Activity>, IActivityServices
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityServices(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            base.CurrentRepository = _activityRepository;
        }
    }
}

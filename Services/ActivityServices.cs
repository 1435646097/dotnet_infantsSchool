using IRepository;
using IServices;
using Model.Entitys;

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
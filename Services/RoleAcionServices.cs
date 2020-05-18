using IRepository;
using IServices;
using Model.Entitys;

namespace Services
{
    public class RoleAcionServices : BaseServices<RoleAction>, IRoleActionServices
    {
        private readonly IRoleActionRepository _roleAcionRepository;

        public RoleAcionServices(IRoleActionRepository roleAcionRepository)
        {
            _roleAcionRepository = roleAcionRepository;
            base.CurrentRepository = _roleAcionRepository;
        }
    }
}

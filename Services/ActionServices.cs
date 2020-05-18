using IRepository;
using IServices;
using Model.Entitys;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ActionServices : BaseServices<Model.Entitys.Action>, IActionServices
    {
        private readonly IActionRepository _actionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleActionRepository _roleActionRepository;

        public ActionServices(IActionRepository actionRepository, IRoleRepository roleRepository, IRoleActionRepository roleActionRepository)
        {
            _actionRepository = actionRepository;
            _roleRepository = roleRepository;
            _roleActionRepository = roleActionRepository;
            base.CurrentRepository = _actionRepository;
        }
        public async Task<bool> SetRoleAction(int roleId, IEnumerable<int> actionId)
        {
            Role role = await _roleRepository.GetEntityByIdAsync(roleId);
            IEnumerable<RoleAction> roleActions = role.RoleAction;
            foreach (var item in roleActions)
            {
                _roleActionRepository.DeleteEntity(item);
            }
            foreach (var item in actionId)
            {
                RoleAction roleAction = new RoleAction() { RoleId = roleId, ActionId = item };
                _roleActionRepository.AddEntityAsync(roleAction);
            }
            return await _roleActionRepository.SaveChangesAsync();
        }
    }
}

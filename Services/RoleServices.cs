using IRepository;
using IServices;
using Model.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class RoleServices : BaseServices<Role>, IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleActionRepository _roleActionRepository;

        public RoleServices(IRoleRepository roleRepository, IRoleActionRepository roleActionRepository)
        {
            _roleRepository = roleRepository;
            _roleActionRepository = roleActionRepository;
            base.CurrentRepository = _roleRepository;
        }

        public async Task<bool> DeleteRoleAndAction(int id)
        {
            Role role = await _roleRepository.GetEntityByIdAsync(id);
            IEnumerable<RoleAction> roleActions = role.RoleAction;
            foreach (var item in roleActions)
            {
                _roleActionRepository.DeleteEntity(item);
            }
            _roleRepository.DeleteEntity(role);
            return await _roleRepository.SaveChangesAsync();
        }
    }
}

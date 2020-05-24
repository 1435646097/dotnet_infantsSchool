using IRepository;
using IServices;
using Microsoft.EntityFrameworkCore;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserRoleServices : BaseServices<UserRole>, IUserRoleServices
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleServices(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
            base.CurrentRepository = _userRoleRepository;
        }
        public async Task<bool> AddUserRoles(int accountId, IEnumerable<int> roleIds)
        {
            IEnumerable<UserRole> userRoles = await _userRoleRepository.GetEntitys().Where(u => u.AccountId == accountId).ToListAsync();
            foreach (var item in userRoles)
            {
                _userRoleRepository.DeleteEntity(item);
            }
            foreach (var item in roleIds)
            {
                 _userRoleRepository.AddEntityAsync(new UserRole()
                {
                    AccountId = accountId,
                    RoleId = item
                });
            }
            return await _userRoleRepository.SaveChangesAsync();
        }
    }
}

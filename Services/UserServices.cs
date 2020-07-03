using IRepository;
using IServices;
using Model.Entitys;
using Model.Helper;
using Model.Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class UserServices : BaseServices<User>, IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserServices(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            base.CurrentRepository = this._userRepository;
        }

        public async Task<PagedList<User>> GetUserPagedAsync(UserParams userParams)
        {
            IQueryable<User> items = this._userRepository.GetEntitys();
            if (!string.IsNullOrWhiteSpace(userParams.Name))
            {
                items = items.Where(u => u.Name.Contains(userParams.Name));
            }
            PagedList<User> list = await PagedList<User>.CreatePagedList(items, userParams.PageSize, userParams.PageNum);
            return list;
        }

        public async Task<bool> deleteUserAsync(int id)
        {
            User entity = await _userRepository.GetEntityByIdAsync(id);
            List<UserRole> userRoles = entity.UserRole.ToList();
            foreach (UserRole item in userRoles)
            {
                _userRoleRepository.DeleteEntity(item);
            }
            _userRepository.DeleteEntity(entity);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
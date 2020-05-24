using Model.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserRoleServices:IBaseIServices<UserRole>
    {
        Task<bool> AddUserRoles(int accountId, IEnumerable<int> roleIds);
    }
}

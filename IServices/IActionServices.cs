using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IActionServices:IBaseIServices<Model.Entitys.Action>
    {
        Task<bool> SetRoleAction(int roleId, IEnumerable<int> actionId);
    }
}

using Model.Helper;
using Model.Params;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IActionServices:IBaseIServices<Model.Entitys.Action>
    {
        Task<PagedList<Model.Entitys.Action>> GetActionPaged(ActionParams actionParams);
        Task<bool> SetRoleAction(int roleId, IEnumerable<int> actionId);
        Task<bool> DeleteAction(int id);
    }
}

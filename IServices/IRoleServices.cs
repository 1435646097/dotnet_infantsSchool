using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IRoleServices:IBaseIServices<Role>
    {
        Task<bool> DeleteRoleAndAction(int id);
    }
}

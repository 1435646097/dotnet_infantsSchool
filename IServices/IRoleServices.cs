using Model.Entitys;
using System.Threading.Tasks;

namespace IServices
{
    public interface IRoleServices : IBaseIServices<Role>
    {
        Task<bool> DeleteRoleAndAction(int id);
    }
}
using Model.Entitys;
using Model.Helper;
using Model.Params;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserServices : IBaseIServices<User>
    {
        Task<PagedList<User>> GetUserPagedAsync(UserParams userParams);
    }
}
using IRepository;
using IServices;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
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

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            base.CurrentRepository = this._userRepository;
        }

        public async Task<PagedList<User>> GetUserPagedAsync(UserParams userParams)
        {
            IQueryable<User> items = this._userRepository.GetEntitys();
            PagedList<User> list = await PagedList<User>.CreatePagedList(items, userParams.PageSize, userParams.PageNum);
            return list;
        }
    }
}
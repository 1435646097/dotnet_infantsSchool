using IRepository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IServices
{
    public interface IBaseIServices<T> where T : class
    {
        IBaseRepository<T> CurrentRepository { get; set; }

        Task<bool> AddEntityAsync(T entity);

        Task<bool> DeleteEntityAsync(T entity);

        Task<bool> EditEntityAsync(T entity);

        Task<bool> ExistEntityAsync(Expression<Func<T, bool>> whereLamda);

        Task<T> GetEntityByIdAsync(int id);

        IQueryable<T> GetEntitys();
    }
}
using IRepository;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaseServices<T> : IBaseIServices<T> where T : class
    {
        public IBaseRepository<T> CurrentRepository { get; set; }

        public async Task<bool> AddEntityAsync(T entity)
        {
            this.CurrentRepository.AddEntityAsync(entity);
            return await CurrentRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteEntityAsync(T entity)
        {
            this.CurrentRepository.DeleteEntity(entity);
            return await CurrentRepository.SaveChangesAsync();
        }

        public async Task<bool> EditEntityAsync(T entity)
        {
            this.CurrentRepository.EditEntity(entity);
            return await CurrentRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistEntityAsync(Expression<Func<T, bool>> whereLamda)
        {
            return await this.CurrentRepository.ExistEntityAsync(whereLamda);
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            return await this.CurrentRepository.GetEntityByIdAsync(id);
        }

        public IQueryable<T> GetEntitys()
        {
            return this.CurrentRepository.GetEntitys();
        }
    }
}
using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public InfantsSchoolSystemContext InfantsSchoolSystemContext { get; set; }

        public BaseRepository(InfantsSchoolSystemContext infantsSchoolSystemContext)
        {
            this.InfantsSchoolSystemContext = infantsSchoolSystemContext;
        }

        public async void AddEntityAsync(T entity)
        {
            await InfantsSchoolSystemContext.Set<T>().AddAsync(entity);
        }

        public void DeleteEntity(T entity)
        {
            InfantsSchoolSystemContext.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void EditEntity(T entity)
        {
            InfantsSchoolSystemContext.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<bool> ExistEntityAsync(Expression<Func<T, bool>> whereLamda)
        {
            return await InfantsSchoolSystemContext.Set<T>().AnyAsync(whereLamda);
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            return await InfantsSchoolSystemContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetEntitys()
        {
            return InfantsSchoolSystemContext.Set<T>();
        }

        public async Task<bool> SaveChangesAsync()
        {
            int count = await InfantsSchoolSystemContext.SaveChangesAsync();
            return count > 0;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

       
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}

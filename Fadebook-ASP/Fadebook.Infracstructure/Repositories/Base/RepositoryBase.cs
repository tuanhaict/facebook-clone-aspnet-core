using Fadebook.Application.Interfaces.Repositories.Base;
using Fadebook.Infracstructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Repositories.Base
{
    public class RepositoryBase<T> : IRepository<T> where T: class 
    {
        private readonly ApplicationContext _context;
        public RepositoryBase(ApplicationContext context) 
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
           _context.Set<T>().Remove(entity);       
        }

        public async Task<IReadOnlyList<T>> GetAllASync()
        {
            return await _context.Set<T>().Take<T>(20).ToListAsync();   
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<IReadOnlyList<T>> GetAllASync();
        Task<T> GetByIdAsync(Guid id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

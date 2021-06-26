using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Contracts.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetById(long id);
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }

    
}
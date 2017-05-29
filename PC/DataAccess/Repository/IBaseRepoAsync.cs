using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.DataAccess.Repository
{
    public interface IBaseRepoAsync<T>
    {
        Task AddAsync(T model);
        IEnumerable<T> GetAllAsync();
        Task<T> GetAsync(int id);
        Task RemoveAsync(int id);
        Task UpdateAsync(T model);
        Task DeactivateAsync(int id);
        Task ActivateAsync(int id);
    }
}

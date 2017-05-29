using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.DataAccess.Repository
{
    class UserRepository : IBaseRepoAsync<User>
    {
        public Task ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(User model)
        {
            throw new NotImplementedException();
        }

        public Task DeactivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User model)
        {
            throw new NotImplementedException();
        }
    }
}

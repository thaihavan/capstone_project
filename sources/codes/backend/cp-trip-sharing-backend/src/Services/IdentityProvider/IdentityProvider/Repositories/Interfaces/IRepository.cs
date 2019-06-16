using IdentityProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Repositories.Interfaces
{
    public interface IRepository<T> where T : Model
    {
        IEnumerable<T> GetAll();

        T Get(string id);

        bool Add(T account);

        bool Delete(string id);

        bool Update(T account);
    }
}

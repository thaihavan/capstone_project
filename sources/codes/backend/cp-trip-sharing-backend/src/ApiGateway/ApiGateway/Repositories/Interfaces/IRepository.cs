using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Repositories.Interfaces
{
    public interface IRepository<T> where T : Model
    {
        IEnumerable<T> GetAll();

        T Get(string id);

        T Add(T param);

        bool Delete(string id);

        T Update(T param);
    }
}

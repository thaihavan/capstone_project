using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Models;

namespace User.Repositories
{
    public interface IRepository<T> where T:Model
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        T Add(T user);

        T Update(T user);

        

    }
}

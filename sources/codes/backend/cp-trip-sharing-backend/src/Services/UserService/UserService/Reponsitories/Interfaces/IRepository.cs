using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IRepository<T> where T : Model
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        T Add(T document);

        T Update(T document);

        T Delete(T document);

    }
}

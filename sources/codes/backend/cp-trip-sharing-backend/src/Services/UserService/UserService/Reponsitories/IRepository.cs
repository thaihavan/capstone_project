using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Reponsitories
{
    interface IRepository<T> where T : Model
    {
        IEnumerable<T> GetAll(string id);

        T GetById(string id);

        bool Add(T document);

        bool Update(T document);

        bool Delete(T document);

    }
}

using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories.Interfaces
{
    public interface IRepository<T> where T : Model
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        T Add(T param);

        bool Delete(string id);

        T Update(T param);
    }
}

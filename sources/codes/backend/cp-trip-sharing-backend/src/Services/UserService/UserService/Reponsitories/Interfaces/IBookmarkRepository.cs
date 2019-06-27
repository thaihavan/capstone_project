using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IBookmarkRepository : IRepository<Bookmark>
    {
        IEnumerable<Bookmark> GetAll(string id);
        IEnumerable<string> GetUserBookmarkId(string id);
    }
}
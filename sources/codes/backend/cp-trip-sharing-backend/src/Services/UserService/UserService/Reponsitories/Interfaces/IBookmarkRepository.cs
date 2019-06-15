using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IBookmarkRepository : IRepository<Bookmark>
    {
        Bookmark Add(Bookmark bookmark);
        Bookmark Delete(Bookmark document);
        IEnumerable<Bookmark> GetAll(string id);
        Bookmark GetById(string user_id);
    }
}
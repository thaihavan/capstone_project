using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IBookmarkRepository:IRepository<Bookmark>
    {
        IEnumerable<Bookmark> GetAll(string id, int page);
        IEnumerable<string> GetUserBookmarkId(string id);
    }
}

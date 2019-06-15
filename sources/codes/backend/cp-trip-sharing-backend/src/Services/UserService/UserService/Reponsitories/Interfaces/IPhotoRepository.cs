using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        Photo Add(Photo document);
        IEnumerable<Photo> GetAll(string userId);
    }
}
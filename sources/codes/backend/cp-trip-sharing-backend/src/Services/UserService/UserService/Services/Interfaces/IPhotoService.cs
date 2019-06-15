using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IPhotoService
    {
        Photo AddPhoto(Photo photo);
        IEnumerable<Photo> GetAllPhoto(string userId);
    }
}
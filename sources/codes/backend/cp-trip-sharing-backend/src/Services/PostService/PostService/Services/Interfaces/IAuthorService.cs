using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IAuthorService
    {
        Author GetById(string id);

        Author Add(Author author);
    }
}

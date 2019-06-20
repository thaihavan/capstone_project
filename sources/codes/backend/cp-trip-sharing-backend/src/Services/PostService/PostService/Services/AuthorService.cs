using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository = null;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Author Add(Author author)
        {
            return _authorRepository.Add(author);
        }

        public Author GetById(string id)
        {
            return _authorRepository.GetById(id);
        }
    }
}

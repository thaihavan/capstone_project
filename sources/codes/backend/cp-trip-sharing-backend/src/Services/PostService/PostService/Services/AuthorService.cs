using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
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

        private readonly IOptions<AppSettings> _settings = null;

        public AuthorService(IOptions<AppSettings> settings, IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _settings = settings;
        }

        public AuthorService(IOptions<AppSettings> settings)
        {
            _authorRepository = new AuthorRepository(settings);
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

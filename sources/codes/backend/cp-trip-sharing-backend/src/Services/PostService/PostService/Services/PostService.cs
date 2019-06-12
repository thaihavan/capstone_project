using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Services.Interfaces;

namespace PostService.Services
{
    public class PostService : IPostService
    {

        private readonly PostRepository _postRepository = null;
        private readonly IOptions<AppSettings> _settings = null;

        public PostService(IOptions<AppSettings> settings)
        {
            _postRepository = new PostRepository(settings);
            _settings = settings;
        }

        public Post Add(Post param)
        {
            return _postRepository.Add(param);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public Post GetById(string id)
        {
            return _postRepository.GetById(id);
        }
    }
}

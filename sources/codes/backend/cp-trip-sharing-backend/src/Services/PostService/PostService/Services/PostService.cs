using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;

namespace PostService.Services
{
    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository = null;
        private readonly IPublishToTopic _publishToTopic = null;

        public PostService(IOptions<AppSettings> settings)
        {
            _postRepository = new PostRepository(settings);
            _publishToTopic = new PublishToTopic();
        }

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Post Add(Post param)
        {
            //_publishToTopic.PublishCP(new IncreasingCP()
            //{
            //    UserId = param.AuthorId,
            //    Point = 1
            //});
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

        public Post Update(Post post)
        {
            return _postRepository.Update(post);
        }

        public bool Delete(string id)
        {
            return _postRepository.Delete(id);
        }
    }
}

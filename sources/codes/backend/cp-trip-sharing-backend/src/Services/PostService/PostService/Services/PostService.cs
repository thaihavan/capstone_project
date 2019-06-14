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
        private readonly VirtualTripRepository _virtualTripRepository = null;

        public PostService()
        {
            _postRepository = new PostRepository();
        }

        public Post AddPost(Post param)
        {
            return _postRepository.Add(param);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public Post GetPostById(string id)
        {
            return _postRepository.GetById(id);
        }

        public VirtualTrip AddVirtualTrip(VirtualTrip param)
        {
            return _virtualTripRepository.Add(param);
        }

        public IEnumerable<VirtualTrip> GetAllTrip()
        {
            return _virtualTripRepository.GetAll();
        }

    }
}

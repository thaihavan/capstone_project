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
        private readonly IArticleRepository _articleRepository = null;
        private readonly ICompanionPostRepository _companionPostRepository = null;
        private readonly IVirtualTripRepository _virtualTripRepository = null;
        private readonly IPublishToTopic _publishToTopic = null;

        public PostService(IOptions<AppSettings> settings)
        {
            _postRepository = new PostRepository(settings);
            _articleRepository = new ArticleRepository(settings);
            _companionPostRepository = new CompanionPostRepository(settings);
            _virtualTripRepository = new VirtualTripRepository(settings);
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

        public object GetAllPostStatistics(StatisticsFilter filter)
        {

            filter.From = filter.From.Date.AddHours(12);
            filter.To = filter.To.Date.AddDays(1).AddHours(12);

            var articleStatistics = _articleRepository.GetArticleStatistics(filter);
            var companionPostStatistics = _companionPostRepository.GetCompanionPostStatistics(filter);
            var virtualTripStatistic = _virtualTripRepository.GetVirtualTripStatistics(filter);

            var result = new List<object>();
            result.Add(articleStatistics);
            result.Add(companionPostStatistics);
            result.Add(virtualTripStatistic);

            return result;
        }
    }
}

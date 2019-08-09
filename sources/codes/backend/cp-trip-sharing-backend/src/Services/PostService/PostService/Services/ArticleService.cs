using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository = null;

        public ArticleService(IOptions<AppSettings> settings)
        {
            _articleRepository = new ArticleRepository(settings);
        }

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Article Add(Article article)
        {
            return _articleRepository.Add(article);
        }

        public bool Delete(string id)
        {
            return _articleRepository.Delete(id);
        }

        public Article GetArticleById(string id, string userId)
        {
            return _articleRepository.GetArticleById(id, userId);
        }

        public Article Update(Article article)
        {
            return _articleRepository.Update(article);
        }

        public IEnumerable<Article> GetAllArticles(PostFilter postFilter, int page)
        {
            return _articleRepository.GetAllArticles(postFilter, page);
        }

        public IEnumerable<Article> GetAllArticlesByUser(string userId, PostFilter postFilter, int page)
        {
            return _articleRepository.GetAllArticlesByUser(userId, postFilter, page);
        }

        public IEnumerable<Article> GetRecommendArticles(PostFilter postFilter, UserInfo userInfo, int page)
        {
            // TODO: Get UserInfo
            GetUserTopics(userInfo);
            GetFollowings(userInfo);

            // TODO: Query Db
            return _articleRepository.GetRecommendArticles(postFilter, userInfo, page);
        }

        public IEnumerable<Article> GetPopularArticles(PostFilter postFilter, int page)
        {
            return _articleRepository.GetPopularArticles(postFilter, page);
        }

        private void GetFollowings(UserInfo userInfo)
        {
            //string baseUrl = "https://localhost:44351/api/userservice/follow/followingids?userId=";
            string baseUrl = "http://35.187.244.118/api/userservice/follow/followingids?userId=";
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(baseUrl
              + userInfo.Id);
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server. 
            // The using block ensures the stream is automatically closed. 
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    var json = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<List<string>>(json);
                    userInfo.Follows = result;
                }
            }
        }

        private void GetUserTopics(UserInfo userInfo)
        {
            //string baseUrl = "https://localhost:44351/api/userservice/user?userId=";
            string baseUrl = "http://35.187.244.118/api/userservice/user?userId=";
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(baseUrl
              + userInfo.Id);      
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server. 
            // The using block ensures the stream is automatically closed. 
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                using (StreamReader reader= new StreamReader(dataStream))
                {
                    var json = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<UserInfo>(json);
                    userInfo.Topics = result.Topics;
                }
            }
        }
    
    }
}

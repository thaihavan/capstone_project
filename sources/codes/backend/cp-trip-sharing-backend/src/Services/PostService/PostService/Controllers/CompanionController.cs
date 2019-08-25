using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using PostService.Models;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    public class CompanionController : ControllerBase
    {
        private readonly ICompanionPostService _companionPostService = null;
        private readonly IPostService _postService = null;

        public CompanionController(ICompanionPostService companionPostService, IPostService postService)
        {
            _companionPostService = companionPostService;
            _postService = postService;
        }

        [Authorize(Roles ="member")]
        [HttpPost("post/create")]
        public async Task<IActionResult> CreateAsync([FromBody]CompanionPost param )
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            var token = Request.Headers["Authorization"].ToString();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    //httpClient.BaseAddress = new Uri("https://localhost:44360/");
                    httpClient.BaseAddress = new Uri("http://35.187.244.118");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Split(' ')[1]);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var conversation = new
                    {
                        name = param.Post.Title
                    };
                    var convJson = JsonConvert.SerializeObject(conversation);
                    var response = await httpClient.PostAsync(
                        "api/chatservice/chat/create-group-chat", 
                        new StringContent(convJson, UnicodeEncoding.UTF8, "application/json")
                        );
                    var conversationId = await response.Content.ReadAsStringAsync();
                    conversationId = conversationId.Replace("\"", "");
                    
                    param.Id = ObjectId.GenerateNewId().ToString();
                    param.Post.AuthorId = userId;
                    param.Post.Id = param.Id;
                    param.PostId = param.Id;
                    param.Post.PubDate = DateTime.Now;
                    param.ConversationId = conversationId;
                    param.Post.IsActive = true;
                    param.Post.PostType = "CompanionPost";
                    param.Post.LikeCount = 0;
                    param.Post.CommentCount = 0;

                    _postService.Add(param.Post);
                    var result = _companionPostService.Add(param);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
        }

        [Authorize(Roles = "member")]
        [HttpPut("post/update")]
        public IActionResult Update([FromBody]CompanionPost param)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            //generate new postid and new conversationid 
            if (!userId.Equals(param.Post.AuthorId))
            {
                return Unauthorized();
            }

            _postService.Update(param.Post);
            var result = _companionPostService.Update(param);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("post")]
        public IActionResult GetCompanionPostById([FromQuery]string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = User.Identity.IsAuthenticated? identity.FindFirst("user_id").Value:null;
            var result = _companionPostService.GetById(id,userId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("post/all")]
        public IActionResult GetAllCompanionPost([FromBody]PostFilter filter,[FromQuery]int page)
        {
            var result = _companionPostService.GetAll(filter, page);
            return Ok(result);
        }

        [Authorize(Roles ="member")]
        [HttpDelete("post")]
        public IActionResult DeleteCompanionPost([FromQuery]string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = User.Identity.IsAuthenticated ? identity.FindFirst("user_id").Value : "";
            var post = _companionPostService.GetById(id);
            if (!userId.Equals(post.Post.AuthorId)) return Unauthorized();
            var result = _companionPostService.Delete(id);
            return Ok(new { Message = "Success" });
        }

        [Authorize(Roles = "member")]
        [HttpPost("post/join")]
        public IActionResult JoinCompanion([FromBody]CompanionPostJoinRequest request)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            request.UserId = userId;
            var result=_companionPostService.AddNewRequest(request);
            
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [HttpGet("post/requests")]
        public IActionResult GetAllRequest([FromQuery]string companionPostId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            var companionPost = _companionPostService.GetById(companionPostId);
            if (!companionPost.Post.AuthorId.Equals(userId))
            {
                return Unauthorized();
            }else
            {
                return Ok(_companionPostService.GetAllJoinRequest(companionPostId));
            }           
        }

        [Authorize(Roles = "member")]
        [HttpDelete("post/request")]
        public  IActionResult DeleteRequest([FromBody]CompanionPostJoinRequest request)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            var companionPost = _companionPostService.GetById(request.CompanionPostId);

            if (!companionPost.Post.AuthorId.Equals(userId))
            {
                return Unauthorized();
            }
            else
            {
                _companionPostService.DeleteJoinRequest(request.Id);
                return Ok();
            }
        }

        [Authorize(Roles = "member")]
        [HttpDelete("post/request/cancel")]
        public IActionResult CancelRequest([FromQuery]string postId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            return Ok(_companionPostService.CancelRequest(userId, postId));
        }

        [AllowAnonymous]
        [HttpPost("post/user")]
        public IActionResult GetAllCompanionPostByUser([FromBody]PostFilter filter, [FromQuery]string userId, [FromQuery]int page)
        {
            return Ok(_companionPostService.GetAllCompanionPostByUser(userId, filter, page));
        }
       
    }
}
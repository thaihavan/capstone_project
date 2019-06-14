using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();

        Post GetPostById(string id);

        Post AddPost(Post param);

        VirtualTrip AddVirtualTrip(VirtualTrip param);
    }
}

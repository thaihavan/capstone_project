using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IPublishToTopic
    {
        void PublishCP(IncreasingCP increasingCP);
    }
}

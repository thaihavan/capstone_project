using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IPublishToTopic
    {
        void PublishAuthor(Author author);
    }
}

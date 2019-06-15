using IdentityProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Services.Interfaces
{
    public interface IPublishToTopic
    {
        Task<bool> PublishEmail(Mail mail);
    }
}

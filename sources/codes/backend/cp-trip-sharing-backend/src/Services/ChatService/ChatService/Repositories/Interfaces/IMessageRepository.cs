using ChatService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<MessageDetail>
    {
    }
}

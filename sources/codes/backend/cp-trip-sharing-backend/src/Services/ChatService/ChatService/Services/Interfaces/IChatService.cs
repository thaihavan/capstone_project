using System.Collections.Generic;
using ChatService.Models;

namespace ChatService.Services.Interfaces
{
    public interface IChatService
    {
        IEnumerable<MessageDetail> GetByConversationId(string id);
        IEnumerable<Conversation> GetByUserId(string id);
        MessageDetail AddMessage(string receiverId, MessageDetail message);
    }
}
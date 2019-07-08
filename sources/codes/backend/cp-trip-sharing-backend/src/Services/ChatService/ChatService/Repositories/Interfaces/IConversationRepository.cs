using ChatService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Repositories.Interfaces
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        IEnumerable<MessageDetail> GetMessageByConversationId(string id);

        IEnumerable<Conversation> GetByUserId(string id);

        IEnumerable<User> GetAllUserInConversation(string conversationId);

        Conversation FindPrivateConversationByReceiverId(string receiverId);

        bool UpdateLastMessage(string conversationId, string lastMessage);

        bool AddUserToGroupChat(string conversationId, string userId);
    }
}

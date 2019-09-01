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

        Conversation FindPrivateConversationByReceiverId(string senderId, string receiverId);

        User AddUserToGroupChat(string conversationId, string userId);

        bool RemoveUserFromGroupChat(string conversationId, string userId);

        bool UpdateSeenIds(string conversationId, List<string> seenIds);

        bool AddToSeenIds(string conversationId, string userId);
    }
}

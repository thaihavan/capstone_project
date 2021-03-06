﻿using System.Collections.Generic;
using ChatService.Models;

namespace ChatService.Services.Interfaces
{
    public interface IChatService
    {
        IEnumerable<MessageDetail> GetByConversationId(string id);

        IEnumerable<Conversation> GetByUserId(string id);

        MessageDetail AddMessage(string senderId, string receiverId, MessageDetail message);

        Conversation CreateGroupChat(Conversation conversation);

        User AddUserToGroupChat(string conversationId, string userId);

        bool RemoveUserFromGroupChat(string conversationId, string userId);

        bool AddToSeenIds(string conversationId, string userId);

        Conversation GetById(string id);

        IEnumerable<User> GetAllMember(string conversationId);
    }
}
﻿using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IBlockService
    {
        Block Block(Block block);

        Block UnBlock(Block block);

        IEnumerable<User> GetBlockedUsers(string blockerId);

        IEnumerable<User> GetBlockers(string userId);
    }
}
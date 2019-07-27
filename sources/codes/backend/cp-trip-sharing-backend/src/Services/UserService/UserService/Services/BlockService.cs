using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services.Interfaces;

namespace UserServices.Services
{
    public class BlockService : IBlockService
    {
        private readonly IBlockRepository _blockRepository = null;

        public BlockService(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }

        public BlockService(IOptions<AppSettings> settings)
        {
            _blockRepository = new BlockRepository(settings);
        }

        public Block Block(Block block)
        {
            return _blockRepository.Add(block);
        }

        public IEnumerable<User> GetBlockedUsers(string blockerId)
        {
            return _blockRepository.GetBlockedUsers(blockerId);
        }

        public Block UnBlock(Block block)
        {
            return _blockRepository.Delete(block);
        }

    }
}
